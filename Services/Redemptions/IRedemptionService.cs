using Coupons.Models;

namespace Coupons.Services.Redemptions
{
    public interface IRedemptionService
    {
        // Method to redeem a coupon asynchronously
        Task<CouponUsageRedeemDTO> RedeemCoupon(string codeCoupon, string username);
    }
}