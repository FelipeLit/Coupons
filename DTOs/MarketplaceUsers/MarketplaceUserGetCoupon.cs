namespace Coupons.Models
{
    public class MarketplaceUserGetCoupon
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public ICollection<CouponUsageGetDTO>? CouponUsages { get; set; }
    }
}