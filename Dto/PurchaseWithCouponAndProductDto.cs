using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coupons.Dto
{
    public class PurchaseWithCouponAndProductDto
    {
        public int PurchaseId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public string? CouponName { get; set; }
    public string? CouponDescription { get; set; }
    public string? ProductName { get; set; }
    public int MarketplaceUserId { get; set; }
    }
}