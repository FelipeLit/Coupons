using AutoMapper;
using Coupons.Data;
using Coupons.Models;
using Coupons.Services.Validations;
using Microsoft.EntityFrameworkCore;
using Coupons.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coupons.Services.Redemptions
{
    public class RedemptionService : IRedemptionService
    {
        private readonly CouponsContext _context;
        private readonly IMapper _mapper;
        private readonly ICouponRedemptionValidator _couponValidator;

        public RedemptionService(CouponsContext context, IMapper mapper, ICouponRedemptionValidator couponValidator)
        {
            _context = context;
            _mapper = mapper;
            _couponValidator = couponValidator;
        }

          public async Task<CouponUsageRedeemDTO> RedeemCoupon(string codeCoupon, string username)
        {
            var validationError = await _couponValidator.ValidateRedemption(codeCoupon, username);
            if (!string.IsNullOrEmpty(validationError))
            {
                throw new Exception(validationError);
            }

            var coupon = await _context.Coupons
                .Include(c => c.MarketingUser)
                .FirstOrDefaultAsync(c => c.Name == codeCoupon);
            if (coupon == null)
            {
                throw new Exception("Coupon not found.");
            }

            var marketplace = await _context.MarketplaceUsers
                .Include(m => m.Purchases!)
                    .ThenInclude(p => p.Product)          
                .FirstOrDefaultAsync(m => m.Username == username);

            if (marketplace == null)
            {
                throw new Exception("Marketplace user not found.");
            }

            var purchase = marketplace.Purchases?
                                    .FirstOrDefault(c => c.MarketplaceUserId == marketplace.Id);

            if (purchase == null)
            {
                throw new Exception("Purchase user not found.");
            }


            var marketingUserName = coupon.MarketingUser?.Username ?? "Unknown"; 
            var emailUser = marketplace.Email ?? "unknown@domain.com"; 
            var productName = purchase.Product?.Name ?? "Unknown Product";

            coupon.UsageLimit++;
            coupon.AmountUses++;

            var newCouponUsage = new CouponUsageEntity
            {
                CouponId = coupon.Id,
                MarketplaceUserId = marketplace.Id,
                UseDate = DateTime.UtcNow
            };

            _context.CouponUsages.Add(newCouponUsage);
            await _context.SaveChangesAsync();



            var mapCoupon = _mapper.Map<CouponUsageRedeemDTO>(newCouponUsage);

            
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == purchase.ProductId);
            decimal Discount = 0;
            // validar tipo descuento
            if (coupon.DiscountType == "Net")
            {
                Discount = (int)(product.Price - coupon.DiscountAmount);
            }
            else if (coupon.DiscountType == "Percentage")
            {
                Discount = product.Price -(product.Price * coupon.DiscountAmount / 100);
            }
            var newPrice = product.Price - Discount;

            var newPurchase = new PurchaseEntity
            {
                MarketplaceUserId = marketplace.Id,
                ProductId = product.Id,
                Date = DateTime.UtcNow,
                Amount = product.Price,
                Discount = Discount,
                Total = newPrice  
            };
            _context.Purchases.Add(newPurchase);
            await _context.SaveChangesAsync();

            var SendEmail = new MailersendUtils();
            await SendEmail.EnviarCorreo(
                marketingUserName,
                emailUser,
                marketplace.Username ?? "unknown_user",
                coupon.Name ?? "Unknown Coupon",
                coupon.Description ?? "No description available",
                newCouponUsage.UseDate,
                productName,
                product.Price,
                Discount,
                newPrice
            );

            return mapCoupon;
        }
    }
}
