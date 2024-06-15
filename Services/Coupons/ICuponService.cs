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
        
        // Asynchronous method that returns a task completed with a boolean value indicating if the coupon update based on an ID was successful.
        Task<bool> UpdateCoupon(int id, CouponForUserDTO couponForUserDTO);
        
        // Asynchronous method that returns a task completed with the list of coupons created by a certain user (CUPONES CREADOS POR UNO DE MARKETING).
        Task<ICollection<CouponForUserDTO>> GetCreatedCoupons(int marketplaceId);
        Task<ICollection<MarketplaceForUserDTO>> GetUsersWithCouponsAsync();
    }
}
