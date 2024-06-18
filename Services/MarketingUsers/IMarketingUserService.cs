using Coupons.Dto;
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
        
        // Asynchronous method that returns a task completed with the created marketplace entity.
        Task<MarketingUserEntity> CreateMarketingUser(MarketingUserDto marketingUserDto);
        //Change status of marketplace Active to Inactive
        Task<MarketingUserEntity> ChangeStatus(int id);
        //Restore status of marketplace Inactive to active
        Task<MarketingUserEntity> RestoreStatus(int id);
        Task<ICollection<MarketingUserGetDTO>> GetAllMarketingUserRemove();
    }
}