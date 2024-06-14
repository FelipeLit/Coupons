namespace Coupons.Models
{
    public class MarketplaceUserEntity
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        public ICollection<PurchaseEntity>? Purchases { get; set; }
        public ICollection<CouponUsageEntity>? CouponUsages { get; set; }
    }
}