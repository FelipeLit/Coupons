namespace Coupons.Models
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        
        public ICollection<UserRoleEntity>? UserRoles { get; set; }        
    }
}