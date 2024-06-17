using AutoMapper;
using Coupons.Data;
using Coupons.Models;
using Coupons.Services.Validations;
using Microsoft.EntityFrameworkCore;
using System;
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

            var marketplace = await _context.MarketplaceUsers.FirstOrDefaultAsync(c => c.Username == username);
            if (marketplace == null)
            {
                throw new Exception("Marketplace user not found.");
            }

            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Name == codeCoupon);
            if (coupon == null)
            {
                throw new Exception("Coupon not found.");
            }

            coupon.UsageLimit++;
            coupon.AmountUses++;

            var couponUsage = new CouponUsageEntity
            {
                CouponId = coupon.Id,
                MarketplaceUserId = marketplace.Id,
                UseDate = DateTime.UtcNow
            };

            _context.CouponUsages.Add(couponUsage);
            await _context.SaveChangesAsync();

            var mapCoupon = _mapper.Map<CouponUsageRedeemDTO>(couponUsage);

            return mapCoupon;
        }
    }
}
