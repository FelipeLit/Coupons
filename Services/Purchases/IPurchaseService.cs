using Coupons.Dto;
using Coupons.Models;

namespace Coupons.Services.Purchases
{
    public interface IPurchaseService
    {
        //See buys with coupons and products asociate
         Task<ICollection<PurchaseWithCouponAndProductDto>> GetAllCouponsPurchased();
    }
}