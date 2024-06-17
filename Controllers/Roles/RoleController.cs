using Coupons.Services.Roles;
using Microsoft.AspNetCore.Mvc;

namespace Roles
{
    public class RoleController : ControllerBase
    {
        // Declare a read-only variable for the role service
        public readonly IRoleService _service;

        // Constructor that receives the role service as a parameter
        public RoleController(IRoleService service)
        {
            _service = service;
        }
        
        // Endpoint to get all roles
        [HttpGet, Route("api/roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try 
            {
                // Call the service to get all roles
                var roles = await _service.GetAllRoles();

                // Check if the roles list is null or empty
                if (roles == null || roles.Count == 0)
                {
                    // Return a 404 Not Found response with a message
                    return NotFound(new { Message = "No roles found in the database.", StatusCode = 404, CurrentDate = DateTime.Now });
                }

                // Return a 200 OK response with the list of roles
                return Ok(roles);
            } 
            catch (Exception ex) 
            {
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = "Internal Server Error", StatusCode = 500, CurrentDate = DateTime.Now,  Error = ex.Message });
            }
        }

        // Endpoint to get a role by its ID
        [HttpGet, Route("api/roles/{id}")]
        public async Task<ActionResult> GetRoleById(int id)
        {
            // Validate the ID is a positive integer
            if (id <= 0)
            {
                // Return a 400 Bad Request response with a message
                return BadRequest(new { Message = "Invalid role ID.", StatusCode = 400, CurrentDate = DateTime.Now });
            }
            
            try 
            {
                // Call the service to get the role by ID
                var role = await _service.GetRoleById(id);

                // Check if the role is null
                if (role == null || role.Count == 0)
                {
                    // Return a 404 Not Found response with a message
                    return NotFound(new { Message = "There are no roles with this ID.", StatusCode = 404, CurrentDate = DateTime.Now });                             }

                // Return a 200 OK response with the role
                return Ok(role);
            }
            catch (Exception ex) 
            {
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = " Internal Server Error", StatusCode = 500, CurrentDate = DateTime.Now , ErrorMessage = ex.Message });
            }
        }
    }
}
