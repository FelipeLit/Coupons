using AutoMapper;
using Coupons.Data;
using Coupons.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Coupons.Services.Auth
{
    // Class that implements the authentication repository interface
    public class AuthService : IAuthService
    {
        // Instance variables
        private readonly CouponsContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        // Constructor that receives database context and configuration
        public AuthService(CouponsContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        // Method to authenticate a user with provided credentials
        public MarketingUserEntity AuthenticateUser(string email, string password)
        {
            // Search the user in the database by their email and include related entities
            var marketing = _context.MarketingUsers
                                    .Include(u => u.UserRoles!)
                                        .ThenInclude(ur => ur.Role)
                                    .SingleOrDefault(u => u.Email == email && u.Password == password) ?? throw new Exception("Invalid email or password");  

     
            // If the credentials are valid, returns the authenticated user
            return marketing; 
        }

        // Method to generate an authentication token for a specific user
        public string GenerateAuthToken(MarketingUserEntity marketing)
        {
            // Set up security credentials
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Define token claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, marketing.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, marketing.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // Add role claims
            if (marketing.UserRoles != null)
            {
                foreach (var userRole in marketing.UserRoles)
                {
                    if (userRole.Role != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name!));
                    }
                }
            }

            // Configure the JWT token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            // Return the JWT token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
