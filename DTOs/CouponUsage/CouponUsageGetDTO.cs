namespace Coupons.Models
{
    public class CouponUsageGetDTO
    {
        public DateTime UseDate { get; set; }
        public CouponGetDTO? Coupon { get; set; }
    }
}