namespace Coupons.Models
{
    public class PurchaseCouponEntity
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public int CouponId { get; set; }

        public PurchaseEntity? Purchase { get; set; }
        public CouponEntity? Coupon { get; set; }
    }
}