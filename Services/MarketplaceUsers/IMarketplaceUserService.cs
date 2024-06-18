using Coupons.Dto;
using Coupons.Models;

namespace Coupons.Services.MarketplaceUsers
{
    public interface IMarketplaceUserService
    {
        // Asynchronous method that returns a task completed with the created marketplace entity.
        Task<MarketplaceUserEntity> CreateMarketplaceUser(MarketplaceUserDto marketplaceUserDtoDto);
        //Change status of marketplace Active to Inactive
        Task<MarketplaceUserEntity> ChangeStatus(int id);
        //Restore status of marketplace Inactive to active
        Task<MarketplaceUserEntity> RestoreStatus(int id);
        Task<ICollection<MarketplaceUserEntity>> GetAllMarketplaceRemove();
    }
}