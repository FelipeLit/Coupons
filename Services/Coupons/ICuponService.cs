// Import the namespace where the CouponEntity class is defined.
using Coupons.Models;

// Namespace where the ICouponService interface is defined.
namespace Coupons
{
    // Definition of the ICouponService interface.
    public interface ICouponService
    {
        // Asynchronous method that returns a task completed with a collection of coupon entities.
        Task<ICollection<CouponEntity>> GetAllCoupons();

        // // Asynchronous method that returns a task completed with a coupon entity based on the provided ID.
        // Task<CouponEntity> GetCouponById(int id);

        // // Asynchronous method that returns a task completed with a collection of historical coupon entities based on an ID.
        // Task<ICollection<CouponEntity>> GetCouponHistory(int id);

        // // Asynchronous method that returns a task completed with the created coupon entity.
        // Task<CouponEntity> CreateCoupon(CouponEntity coupon);

        // // Asynchronous method that returns a task completed with the restored coupon entity based on an ID and an updated coupon entity.
        // Task<CouponEntity> RestoreCoupon(int id, CouponEntity updatedCoupon);

        // // Asynchronous method that returns a task completed with a boolean value indicating if the coupon update based on an ID was successful.
        // Task<bool> UpdateCoupon(int id);

        // // Asynchronous method that returns a task completed with a boolean value indicating if the coupon deletion based on an ID was successful.
        // Task<bool> DeleteCoupon(int id);
    }
}
