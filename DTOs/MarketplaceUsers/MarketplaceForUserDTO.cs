namespace Coupons.Models
{
    public class MarketplaceForUserDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public ICollection<CouponUsageForCouponsDTO>? CouponUsages { get; set; }
    }
}