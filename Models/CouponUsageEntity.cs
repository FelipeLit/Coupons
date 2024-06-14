namespace Coupons.Models
{
    public class CouponUsageEntity
    {
        public int Id { get; set; }
        public int CouponId { get; set; }
        public int MarketplaceUserId { get; set; }
        public DateTime UseDate { get; set; }
        public CouponEntity? Coupon { get; set; }
        public MarketplaceUserEntity? MarketplaceUser { get; set; }
    }
}