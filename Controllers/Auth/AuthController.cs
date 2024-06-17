using Coupons.Models;
using Coupons.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        // Declare a read-only variable for the authentication service
        private readonly IAuthService _service;

        // Constructor that injects the authentication service
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        // Allow anonymous access to this method
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] MarketingForLoginDTO loginRequest)
        {
            // Check that the email and password are not empty
            if (string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return BadRequest("Email and Password must not be empty");
            }
            
            try 
            {

                // Authenticate the user
                var marketing = _service.AuthenticateUser(loginRequest.Email, loginRequest.Password);

                // If authentication fails, return Unauthorized
                if (marketing == null)
                {
                    return Unauthorized("Invalid credentials.");
                }

                // Get user roles
                var roles = marketing.UserRoles?.Select(ur => ur.Role?.Name).ToList();

                // Generate an authentication token
                var tokenString = _service.GenerateAuthToken(marketing);

                // Return the token and roles
                return Ok(new { token = tokenString, roles });
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error response with a message
                return BadRequest(new { Message = "Internal Server Error", StatusCode = 500 ,CurrentDate = DateTime.Now,  Error = ex.Message });
            }
        }
    }
}