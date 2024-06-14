namespace Coupons.Models
{
    public class UserRoleEntity
    {
        public int Id { get; set; }
        public int MarketingUserId { get; set; }
        public int RoleId { get; set; }

        public MarketingUserEntity? MarketingUser { get; set; }
        public RoleEntity? Role { get; set; }
    }
}