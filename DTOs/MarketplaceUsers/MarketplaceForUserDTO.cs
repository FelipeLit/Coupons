namespace Coupons.Models
{
    public class MarketplaceUserForUserDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public ICollection<CouponUsageForCouponsDTO>? CouponUsages { get; set; }
    }
}