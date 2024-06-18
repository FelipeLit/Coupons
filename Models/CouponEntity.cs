namespace Coupons.Models
{
    public class CouponEntity
    {
        public int Id { get; set; } // UPDATE, THROW ERROR ID IS EXIST
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? DiscountType { get; set; }
        public bool? IsLimited { get; set;} // True ilimitato , False Limited
            public int UsageLimit { get; set; } // Cuantas veces de usa depende de arriba
                public int AmountUses { get; set; } // Tipo contador de la cantidad de veces
        public decimal MinPurchaseAmount { get; set; }
        public decimal MaxPurchaseAmount { get; set; }
        public string? Status { get; set; }
        public int MarketingUserId { get; set; } // AUTO

        public MarketingUserEntity? MarketingUser { get; set; }
        public ICollection<CouponUsageEntity>? CouponUsages { get; set; }
        public ICollection<PurchaseCouponEntity>? PurchaseWithCouponsDto { get; set; }
        public ICollection<CouponHistoryEntity>? CouponHistories { get; set; }
        
    }
}