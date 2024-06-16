using Coupons.Models;

namespace Coupons.Services.Roles
{
    public interface IRoleService
    {
        // Asynchronous method that returns a task completed with a collection of role entities.
        Task<ICollection<RoleGetDTO>> GetAllRoles();
        // Asynchronous method that returns a task completed with a role entity based on the provided ID.
        Task<ICollection<RoleGetDTO>> GetRoleById(int id);
        // Asynchronous method that returns a task completed with the created(Assing) role entity.
        Task AssignRoleToUser(UserRolePostDTO request);
    }
}