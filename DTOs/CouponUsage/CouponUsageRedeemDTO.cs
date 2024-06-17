using System.ComponentModel.DataAnnotations;

namespace Coupons.Models
{
    public class CouponUsageRedeemDTO
    {
        public int CouponId { get; set; }
        public int MarketplaceUserId { get; set; }
        public DateTime UseDate { get; set; }

    }
    
    // Data transfer object for the coupon redemption request
    public class CouponRedemptionRequest
    {
        [Required(ErrorMessage = "Coupon code is required.")]
        [StringLength(50, ErrorMessage = "Coupon code can't be longer than 50 characters.")]
        public string? CodeCoupon { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(255, ErrorMessage = "Username can't be longer than 255 characters.")]
        public string? Username { get; set; }
    }
}