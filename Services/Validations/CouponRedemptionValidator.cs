using Coupons.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Coupons.Services.Validations
{
    public class CouponRedemptionValidator : ICouponRedemptionValidator
    {
        private readonly CouponsContext _context;

        public CouponRedemptionValidator(CouponsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<string> ValidateRedemption(string codeCoupon, string username)
        {
            var marketplace = await _context.MarketplaceUsers.FirstOrDefaultAsync(c => c.Username == username);
            if (marketplace == null)
            {
                return "Marketplace user not found.";
            }

            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Name == codeCoupon);
            if (coupon == null)
            {
                return "Coupon not found.";
            }

            // Check if the coupon is available for redemption
            if (coupon.UsageLimit <= 0 || coupon.AmountUses >= coupon.UsageLimit)
            {
                return "Coupon is not available for redemption for amount uses.";
            }

            // Check if the coupon is active
            if (coupon.Status == "Inactive")
            {
                return "Coupon is (Inactive) for redemption.";
            }

            // Check if the redemption date is within the valid range
            if (coupon.StartDate > DateTime.UtcNow || coupon.EndDate < DateTime.UtcNow)
            {
                return "Coupon is not valid for redemption at this time.";
            }

            // Validate MinPurchaseAmount and MaxPurchaseAmount
            if (coupon.MinPurchaseAmount >= coupon.MaxPurchaseAmount)
            {
                return "Max purchase amount must be greater than min purchase amount.";
            }

            return string.Empty;
        }
    }
}
