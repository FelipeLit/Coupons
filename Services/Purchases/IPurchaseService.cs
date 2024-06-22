using Coupons.Dto;
using Coupons.DTOs.Purchases;
using Coupons.Models;

namespace Coupons.Services.Purchases
{
    public interface IPurchaseService
    {
        //See buys with coupons and products asociate
         Task<ICollection<PurchaseWithCouponDto>> GetAllCouponsPurchased();
    }
}