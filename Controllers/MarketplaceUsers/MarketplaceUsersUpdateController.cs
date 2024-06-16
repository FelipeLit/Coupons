using Microsoft.AspNetCore.Mvc;
using Coupons.Services.MarketplaceUsers;
using Coupons.Models;

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

         // Endpoint to update a marketplaceUser
        [HttpPut("api/marketplace-users/update/{id}")]
        public async Task<IActionResult> UpdateMarketplaceUser(int id, [FromBody] MarketplaceUserForUserDTO marketplaceUserForUserDTO)
        {
            // Validate that the model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if id is valid
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid marketplaceUser ID. ID must be greater than zero.", StatusCode = 400});
            }

            try
            {
                // Try to update the marketplaceUser
                var result = await _service.UpdateMarketplaceUser(id, marketplaceUserForUserDTO);
                
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