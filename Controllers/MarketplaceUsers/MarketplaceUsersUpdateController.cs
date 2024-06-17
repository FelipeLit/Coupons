using Microsoft.AspNetCore.Mvc;
using Coupons.Services.MarketplaceUsers;
using Coupons.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MarketplaceUsers
{
    public class MarketplaceUsersUpdateController : ControllerBase
    {
        // Declare a read-only variable for the marketplaceUser service
        public readonly IMarketplaceUserService _service;

        // Constructor that receives the marketplaceUser service as a parameter
        public MarketplaceUsersUpdateController(IMarketplaceUserService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Marketplace")]
        // Endpoint to update a marketplaceUser
        [HttpPut("api/marketplace-users/update/{id}")]
        public async Task<IActionResult> UpdateMarketplaceUser(int id, [FromBody] MarketplaceGetDTO MarketplaceGetDTO)
        {
            // Validate that the model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if id is valid
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid marketplaceUser ID. ID must be greater than zero.", MarketplaceId = id, StatusCode = 400});
            }

            try
            {
                // Get the claims of the current user
                var userRolesClaims = User?.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();

                // Check if the user's claims do not exist or are empty
                if (userRolesClaims == null || !userRolesClaims.Any())
                {
                    return Unauthorized("Could not get user information.");
                }

                // Check if the user has the "Marketplace" role
                if (!userRolesClaims.Contains("Marketplace"))
                {
                    return Unauthorized("You don't have permissions (Only Marketplace).");
                }

                // Try to update the marketplaceUser
                var result = await _service.UpdateMarketplaceUser(id, MarketplaceGetDTO);
                
                if (!result)
                {
                    return NotFound(new { Message = $"MarketplaceUser with ID {id} not found.", StatusCode = 404, });
                }

                return Ok(new { Message = "MarketplaceUser updated successfully.", StatusCode = 200 });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return BadRequest(new { Message = "An error occurred while updating the marketplaceUser.", StatusCode = 500, CurrentDate = DateTime.Now, Error = ex.Message });
            }
        }

    }
}