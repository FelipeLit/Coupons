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
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] MarketingForLoginDTO loginRequest)
        {
            try 
            {
                // Verify that the email and password are not empty
                if (string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
                {
                    return BadRequest("Email and Password must not be empty");
                }

                // Authenticate the user
                var marketing = _service.AuthenticateUser(loginRequest.Email, loginRequest.Password);

                if (marketing == null)
                {
                    return Unauthorized("Invalid credentials.");
                }

                var roles = marketing.UserRoles?.Select(ur => ur.Role?.Name).ToList();

                // Verify if the user has any role
                if (roles == null || roles.Count == 0)
                {
                    return Unauthorized("There is no Marketing role available for this user.");
                }

                // Check if the user has the "Marketing" role
                if (!roles.Contains("Marketing"))
                {
                    return Unauthorized("You are not authorized to access.");
                }

                // Generate the authentication token
                var tokenString = _service.GenerateAuthToken(marketing);

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
