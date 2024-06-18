namespace Coupons.Models
{
    public class PurchaseCouponDTO
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public CouponGetDTO? Coupon { get; set; }
    }
}