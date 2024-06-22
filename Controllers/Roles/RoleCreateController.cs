using System.Security.Claims;
using Coupons.Models;
using Coupons.Services.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers.Roles
{
    public class RoleCreateController : ControllerBase
    {
        private readonly IRoleService _service;
        public RoleCreateController(IRoleService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, Route("api/role/create")]
        public async Task<IActionResult> CreateRole([FromBody] RolePostDTO rol)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                // Get the claims of the current user
                var userRolesClaims = User?.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();

                // Check if the user's claims do not exist or are empty
                if (userRolesClaims == null || !userRolesClaims.Any())
                {
                    return Unauthorized("Could not get user information.");
                }

                // Check if the user has the "Admin" role
                if (!userRolesClaims.Contains("Admin"))
                {
                    return Unauthorized("You don't have permissions (Only Admin).");
                }

                await _service.CreateRole(rol);
                return Ok(new { Message = "Role created successfully.", StatusCode = 201, CurrentDate = DateTime.Now});
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return BadRequest(new { Message = "An error occurred while updating the coupon.", StatusCode = 500, CurrentDate = DateTime.Now, Error = ex.Message });            }
        }
    }
}