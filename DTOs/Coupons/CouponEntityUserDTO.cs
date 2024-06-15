using System.ComponentModel.DataAnnotations;

namespace Coupons.Models
{
    public class CouponEntityUserDTO
    {
        [Required]
        public int Id { get; set; } // UPDATE, THROW ERROR ID IS EXIST
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
        public bool? IsLimited { get; set;} // True ilimitato , False Limited
        [Required]
            public int UsageLimit { get; set; } // Cuantas veces de usa depende de arriba
        [Required]
                public int AmountUses { get; set; } // Tipo contador de la cantidad de veces
        [Required]
        public decimal MinPurchaseAmount { get; set; }
        [Required]
        public decimal MaxPurchaseAmount { get; set; }
        [Required]
        public string? Status { get; set; }
        [Required]
        public int MarketingUserId { get; set; } // AUTO
        
    }
}