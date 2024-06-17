namespace Coupons.Models
{
    public class RoleGetDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<UserRoleGetDTO>? UserRoles { get; set; }  
             
    }
}