using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coupons.Dto
{
    public class CouponsDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string? DiscountType { get; set; }
        [Required]
        public bool IsLimited { get; set;}
        [Required]
            public int? UsageLimit { get; set; }
            [Required]
                public int? AmountUses { get; set; }
                [Required]
        public decimal MinPurchaseAmount { get; set; }
        [Required]
        public decimal MaxPurchaseAmount { get; set; }
        [Required]
        public string? Status { get; set; }
        [Required]
        public int MarketingUserId { get; set; }
    }
}