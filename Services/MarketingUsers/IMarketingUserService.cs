using Coupons.Dto;
using Coupons.Models;

namespace Coupons.Services.MarketingUsers
{
    public interface IMarketingUserService
    {
                 // Asynchronous method that returns a task completed with the created marketplace entity.
        Task<MarketingUserEntity> CreateMarketingUser(MarketingUserDto marketingUserDto);
        //Change status of marketplace Active to Inactive
        Task<MarketingUserEntity> ChangeStatus(int id);
        //Restore status of marketplace Inactive to active
        Task<MarketingUserEntity> RestoreStatus(int id);
        Task<ICollection<MarketingUserEntity>> GetAllMarketingUserRemove();
    }
}