namespace Coupons.Models
{
    public class CouponHistoryEntity
    {
        public int Id { get; set; }
        public int CouponId { get; set; }
        public DateTime ChangeDate { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }   

        public CouponEntity? Coupon { get; set; }     
    }
}