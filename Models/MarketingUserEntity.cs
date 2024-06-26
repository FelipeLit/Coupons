namespace Coupons.Models
{
    public class MarketingUserEntity
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public ICollection<CouponEntity>? Coupons { get; set; }
        public ICollection<UserRoleEntity>? UserRoles { get; set; }
    }
}