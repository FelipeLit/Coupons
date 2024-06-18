using Coupons.Models;

namespace Coupons.Services.Auth
{
    public interface IAuthService
    {
        // Authenticates a user with the provided credentials
        MarketingUserEntity AuthenticateUser(string email, string password);

        // Generate an authentication token for a specific user
        string GenerateAuthToken(MarketingUserEntity marketingUser);
    }
}
