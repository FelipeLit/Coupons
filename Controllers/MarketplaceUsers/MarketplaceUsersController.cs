using System.Security.Claims;
using Coupons.Services.MarketplaceUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceUsers
{
    public class MarketplaceUserController : ControllerBase
    {
        // Declare a read-only variable for the marketplaceUser service
        public readonly IMarketplaceUserService _service;

        // Constructor that receives the marketplaceUser service as a parameter
        public MarketplaceUserController(IMarketplaceUserService service)
        {
            _service = service;
        }
        
        [Authorize(Roles = "Admin, Marketing")]
        // Endpoint to get all marketplaceUsers
        [HttpGet, Route("api/marketplace-users")]
        public async Task<IActionResult> GetAllMarketplaceUsers()
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

                // Check if the user has the "Admin or Marketing" role
                if (!userRolesClaims.Contains("Admin") || !userRolesClaims.Contains("Marketing"))
                {
                    return Unauthorized("You don't have permissions (Only Admins or Marketing).");
                }

                // Call the service to get all marketplaceUsers
                var marketplaceUsers = await _service.GetAllMarketplaceUsers();

                // Check if the marketplaceUsers list is null or empty
                if (marketplaceUsers == null || marketplaceUsers.Count == 0)
                {
                    // Return a 404 Not Found response with a message
                    return NotFound(new { Message = "No marketplaceUsers found in the database.", StatusCode = 404, CurrentDate = DateTime.Now });
                }

                // Return a 200 OK response with the list of marketplaceUsers
                return Ok(marketplaceUsers);
            } 
            catch (Exception ex) 
            {
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = "Internal Server Error", StatusCode = 500, CurrentDate = DateTime.Now,  Error = ex.Message });
            }
        }

        // Endpoint to get a marketplaceUser by its ID
        [HttpGet, Route("api/marketplace-users/{id}")]
        public async Task<ActionResult> GetMarketplaceUserById(int id)
        {
            // Validate the ID is a positive integer
            if (id <= 0)
            {
                // Return a 400 Bad Request response with a message
                return BadRequest(new { Message = "Invalid marketplaceUser ID.", StatusCode = 400, CurrentDate = DateTime.Now });
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

                // Check if the user has the "Admin or Marketing" role
                if (!userRolesClaims.Contains("Admin") || !userRolesClaims.Contains("Marketing"))
                {
                    return Unauthorized("You don't have permissions (Only Admins or Marketing).");
                }
                
                // Call the service to get the marketplaceUser by ID
                var marketplaceUser = await _service.GetMarketplaceUserById(id);

                // Check if the marketplaceUser is null
                if (marketplaceUser == null)
                {
                    // Return a 404 Not Found response with a message
                    return NotFound(new { Message = "MarketplaceUser not found in the database.", StatusCode = 404, CurrentDate = DateTime.Now });
                }

                // Return a 200 OK response with the marketplaceUser
                return Ok(marketplaceUser);
            }
            catch (Exception) 
            {
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = " Internal Server Error", StatusCode = 500, CurrentDate = DateTime.Now });
            }
        }

        [HttpGet]
        [Route("marketplace/delete")]
        public async Task<IActionResult> GetAllMarketplaceUsersRemove()
        {
            try 
            {
                var marketplaceUsers = await _service.GetAllMarketplaceRemove();
                if (marketplaceUsers == null || marketplaceUsers.Count == 0)
                {
                    return NotFound(new { Message = "404 No marketplaceUsers found in the database." , currentDate = DateTime.Now});
                }   
                return Ok(marketplaceUsers);
            } 
            catch (Exception) 
            {
                return BadRequest(new { Message = "500 Internal Server Error", currentDate = DateTime.Now});
            }
        }

                // This defines a GET endpoint at "api/coupon-usages".
        

        // Method to obtain the marketplaceUsers created by the authenticated marketing
        // [HttpGet, Route("api/mymarketplaceUsers")]
        // public IActionResult GetMyMarketplaceUsers()
        // {
        //     var userIdClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //     if (userIdClaim == null)
        //     {
        //         return Unauthorized("No se pudo obtener la información del usuario.");
        //     }

        //     var userId = int.Parse(userIdClaim);
        //     var marketplaceUsers = _service.GetCreatedMarketplaceUsers(userId);
        //     return Ok(marketplaceUsers);
        // }


    }
}
