using Coupons.Dto;
using Coupons.Models;

namespace Coupons.Services.MarketplaceUsers
{
    public interface IMarketplaceUserService
    {
        // Asynchronous method that returns a task completed with a collection of marketplaceUser entities.
        Task<ICollection<MarketplaceUserForUserDTO>> GetAllMarketplaceUsers();
        // Asynchronous method that returns a task completed with a marketplaceUser entity based on the provided ID.
        Task<MarketplaceUserForUserDTO> GetMarketplaceUserById(int id);
        // Asynchronous method that returns a task completed with a boolean value indicating if the marketplaceUser update based on an ID was successful.
        Task<bool> UpdateMarketplaceUser(int id, MarketplaceUserForUserDTO marketplaceUserForUserDTO);
        // Asynchronous method that returns a task completed with the created marketplace entity.
        //Task<MarketplaceUserEntity> CreateMarketplaceUser(MarketplaceUserDto marketplaceUserDtoDto);
        //Change status of marketplace Active to Inactive
        Task<MarketplaceUserEntity> ChangeStatus(int id);
        //Restore status of marketplace Inactive to active
        Task<MarketplaceUserEntity> RestoreStatus(int id);
        Task<ICollection<MarketplaceUserEntity>> GetAllMarketplaceRemove();
    }
}