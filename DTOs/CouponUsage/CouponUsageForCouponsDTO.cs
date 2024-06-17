namespace Coupons.Models
{
    public class CouponUsageForCouponsDTO
    {
        public DateTime UseDate { get; set; }
        public CouponForUserDTO? Coupon { get; set; }
    }
}