using Coupons.Models;

namespace Coupons.Services.MarketingUsers
{
    public interface IMarketingUserService
    {
        // Asynchronous method that returns a task completed with a collection of marketingUser entities.
        Task<ICollection<MarketingUserGetDTO>> GetAllMarketingUsers();
        // Asynchronous method that returns a task completed with a marketingUser entity based on the provided ID.
        Task<MarketingUserGetDTO> GetMarketingUserById(int id);
        // Asynchronous method that returns a task completed with a boolean value indicating if the marketingUser update based on an ID was successful.
        Task<bool> UpdateMarketingUser(int id, MarketingUserPutDTO marketingUserPutDTO);
    }
}