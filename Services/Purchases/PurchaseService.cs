using Coupons.Data;
using Coupons.Dto;
using Coupons.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Services.Purchases
{

    public class PurchaseService : IPurchaseService
    {
        private readonly CouponsContext _context;
        

        public PurchaseService(CouponsContext context)
        {
            _context = context;
        }

        public async Task<ICollection<PurchaseWithCouponAndProductDto>> GetAllCouponsPurchased()
        {
            try
            {
                var purchases = await _context.Purchases
                    .Include(p => p.Product) // Ensure Product is included
                    .GroupJoin(
                        _context.PurchaseCoupon.Include(pc => pc.Coupon),
                        purchase => purchase.Id,
                        purchaseCoupon => purchaseCoupon.PurchaseId,
                        (purchase, purchaseCoupons) => new { purchase, purchaseCoupons }
                    )
                    .SelectMany(
                        x => x.purchaseCoupons.DefaultIfEmpty(),
                        (x, purchaseCoupon) => new PurchaseWithCouponAndProductDto
                        {
                            PurchaseId = x.purchase.Id,
                            ProductName = x.purchase.Product.Name,
                            Date = x.purchase.Date,
                            Amount = x.purchase.Amount,
                            Discount = x.purchase.Discount,
                            Total = x.purchase.Total,
                            CouponName = purchaseCoupon != null ? purchaseCoupon.Coupon.Name : null,
                            CouponDescription = purchaseCoupon != null ? purchaseCoupon.Coupon.Description : null,
                            MarketplaceUserId = x.purchase.MarketplaceUserId
                        }
                    )
                    .ToListAsync();

                return purchases;
            }
            catch (DbUpdateException dbEx)
            {
                // Log specific database update exception
                throw new Exception("A database error occurred while retrieving purchases. Please try again later.");
            }
            catch (InvalidOperationException invOpEx)
            {
                // Log specific invalid operation exception
                throw new Exception("An internal error occurred while retrieving purchases. Please try again later.");
            }
            catch (Exception ex)
            {
                // Log general exceptions
                throw new Exception("An error occurred while retrieving purchases. Please try again later.");
            }
        }


    }

}
