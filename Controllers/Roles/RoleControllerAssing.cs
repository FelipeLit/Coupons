using Coupons.Models;
using Coupons.Services.Roles;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers.Roles
{
    public class RoleControllerAssing : ControllerBase
    {
        // Declare a read-only variable for the role service
        public readonly IRoleService _service;

        // Constructor that receives the role service as a parameter
        public RoleControllerAssing(IRoleService service)
        {
            _service = service;
        }
        [HttpPost("api/roles/assign")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] UserRolePostDTO userRoleDto)
        {
            try
            {
                // Call the service to assign the role to the user
                await _service.AssignRoleToUser(userRoleDto);
                
                // Return a 200 OK response with a message
                return Ok(new { Message = "Role assigned successfully", StatusCode = 200, CurrentDate = DateTime.Now });
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = "Error assigning role", StatusCode = 500, CurrentDate = DateTime.Now, Error = ex.Message });
            }
        }

    }
}