using System.ComponentModel.DataAnnotations;

namespace Coupons.Models
{
    public class MarketingWithCoupons
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public ICollection<CouponGetDTO>? Coupons { get; set; }
    }
}