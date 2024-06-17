using System.Security.Claims;
using Coupons.Services.MarketingUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketingUsers
{
    public class MarketingUserController : ControllerBase
    {
        // Declare a read-only variable for the marketingUser service
        public readonly IMarketingUserService _service;

        // Constructor that receives the marketingUser service as a parameter
        public MarketingUserController(IMarketingUserService service)
        {
            _service = service;
        }
        
        // Endpoint to get all marketingUsers
        [Authorize(Roles = "Admin")]
        [HttpGet, Route("api/marketing-users")]
        public async Task<IActionResult> GetAllMarketingUsers()
        {
            try 
            {
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
                    return Unauthorized("You don't have permissions (Only Admins).");
                }
                
                // Call the service to get all marketingUsers
                var marketingUsers = await _service.GetAllMarketingUsers();

                // Check if the marketingUsers list is null or empty
                if (marketingUsers == null || marketingUsers.Count == 0)
                {
                    // Return a 404 Not Found response with a message
                    return NotFound(new { Message = "No marketingUsers found in the database.", StatusCode = 404, CurrentDate = DateTime.Now });
                }

                // Return a 200 OK response with the list of marketingUsers
                return Ok(marketingUsers);
            } 
            catch (Exception ex) 
            {
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = "Internal Server Error", StatusCode = 500, CurrentDate = DateTime.Now,  Error = ex.Message });
            }
        }

        // Endpoint to get a marketingUser by its ID
        [Authorize(Roles = "Admin")]
        [HttpGet, Route("api/marketing-users/{id}")]
        public async Task<ActionResult> GetMarketingUserById(int id)
        {
            // Validate the ID is a positive integer
            if (id <= 0)
            {
                // Return a 400 Bad Request response with a message
                return BadRequest(new { Message = "Invalid marketingUser ID.", StatusCode = 400, MarketingId = id, CurrentDate = DateTime.Now });
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

                // Check if the user has the "Admin" role
                if (!userRolesClaims.Contains("Admin"))
                {
                    return Unauthorized("You don't have permissions (Only Admins).");
                }

                // Call the service to get the marketingUser by ID
                var marketingUser = await _service.GetMarketingUserById(id);

                // Check if the marketingUser is null
                if (marketingUser == null)
                {
                    // Return a 404 Not Found response with a message
                    return NotFound(new { Message = "MarketingUser not found in the database.", StatusCode = 404, CurrentDate = DateTime.Now });
                }

                // Return a 200 OK response with the marketingUser
                return Ok(marketingUser);
            }
            catch (Exception) 
            {
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = " Internal Server Error", StatusCode = 500, CurrentDate = DateTime.Now });
            }
        }

    }
}
