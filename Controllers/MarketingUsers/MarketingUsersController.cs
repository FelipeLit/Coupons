using Coupons.Services.MarketingUsers;
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
        [HttpGet, Route("api/marketing-users")]
        public async Task<IActionResult> GetAllMarketingUsers()
        {
            try 
            {
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
        [HttpGet, Route("api/marketing-users/{id}")]
        public async Task<ActionResult> GetMarketingUserById(int id)
        {
            try 
            {
                // Validate the ID is a positive integer
                if (id <= 0)
                {
                    // Return a 400 Bad Request response with a message
                    return BadRequest(new { Message = "Invalid marketingUser ID.", StatusCode = 400, CurrentDate = DateTime.Now });
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

        // Method to obtain the marketingUsers created by the authenticated marketing
        // [HttpGet, Route("api/mymarketingUsers")]
        // public IActionResult GetMyMarketingUsers()
        // {
        //     var userIdClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //     if (userIdClaim == null)
        //     {
        //         return Unauthorized("No se pudo obtener la información del usuario.");
        //     }

        //     var userId = int.Parse(userIdClaim);
        //     var marketingUsers = _service.GetCreatedMarketingUsers(userId);
        //     return Ok(marketingUsers);
        // }


    }
}
