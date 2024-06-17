namespace Coupons.Models
{
    public class CouponUsageGetDTO
    {
        public DateTime UseDate { get; set; }
        public CouponViewUserDTO? Coupon { get; set; }
    }
}