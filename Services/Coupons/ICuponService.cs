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
        Task<ICollection<CouponEntityUserDTO>> GetAllCoupons();

        // Asynchronous method that returns a task completed with a coupon entity based on the provided ID.
        Task<CouponEntityUserDTO> GetCouponById(int id);

        // // Asynchronous method that returns a task completed with the created coupon entity.
        Task<CouponEntity> CreateCoupon(CouponsDto coupon);
    }
}
