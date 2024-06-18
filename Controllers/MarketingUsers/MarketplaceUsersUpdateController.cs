using Microsoft.AspNetCore.Mvc;
using Coupons.Services.MarketingUsers;
using Coupons.Models;

namespace MarketingUsers
{
    public class MarketingUsersUpdateController : ControllerBase
    {
        // Declare a read-only variable for the marketingUser service
        public readonly IMarketingUserService _service;

        // Constructor that receives the marketingUser service as a parameter
        public MarketingUsersUpdateController(IMarketingUserService service)
        {
            _service = service;
        }

         // Endpoint to update a marketingUser
        [HttpPut("api/marketing-users/update/{id}")]
        public async Task<IActionResult> UpdateMarketingUser(int id, [FromBody] MarketingUserPutDTO marketingUserPutDTO)
        {
            // Validate that the model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if id is valid
            if (id <= 0)
            {
                return BadRequest(new { Message = "Invalid marketingUser ID. ID must be greater than zero.", StatusCode = 400});
            }

            try
            {
                // Try to update the marketingUser
                var result = await _service.UpdateMarketingUser(id, marketingUserPutDTO);
                
                if (!result)
                {
                    return NotFound(new { Message = $"MarketingUser with ID {id} not found.", StatusCode = 404, });
                }

                return Ok(new { Message = "MarketingUser updated successfully.", StatusCode = 200 });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return BadRequest(new { Message = "An error occurred while updating the marketingUser.", StatusCode = 500, CurrentDate = DateTime.Now, Error = ex.Message });
            }
        }

    }
}