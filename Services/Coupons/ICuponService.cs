// Import the namespace where the CouponEntity class is defined.
using Coupons.Dto;
using Coupons.Models;

// Namespace where the ICouponService interface is defined.
namespace Coupons
{
    // Definition of the ICouponService interface.
    public interface ICouponService
    {
        // Asynchronous method that returns a task completed with a collection of coupon entities.
        Task<ICollection<CouponForUserDTO>> GetAllCoupons();

        // Asynchronous method that returns a task completed with a coupon entity based on the provided ID.
        Task<CouponForUserDTO> GetCouponById(int id);

        // Asynchronous method that returns a task completed with the created coupon entity.
        Task<CouponEntity> CreateCoupon(CouponsDto coupon);


        //Change status of coupon Active to Inactive
        Task<CouponEntity> ChangeStatus(int id);
        //Restore status of coupon Inactive to active
        Task<CouponEntity> RestoreStatus(int id);
        //View All  coupons remove
        Task<ICollection<CouponsDto>> GetAllCouponsRemove();

        //PURCHASECOUPONS see all buys and coupons asociate
        Task<ICollection<PurchaseCouponEntity>> GetAllCouponsPurchased();

    }
        
   }

