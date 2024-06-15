using Coupons.Data;
using Coupons.Dto;
using Coupons.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Transactions;

namespace Coupons
{
    // Implementation of ICouponService interface for managing coupons
    public class CouponService : ICouponService
    {
        // Private variable to hold the database context
        private readonly CouponsContext _context;

        // Constructor injecting the database context dependency
        public CouponService(CouponsContext context)
        {
            _context = context;
        }

        public async Task<CouponEntity> CreateCoupon(CouponsDto couponDto)
        {
            try
            {
                var coupon = new CouponEntity
                {
                    Name = couponDto.Name,
                    Description = couponDto.Description,
                    StartDate = couponDto.StartDate,
                    EndDate = couponDto.EndDate,
                    DiscountType = couponDto.DiscountType,
                    IsLimited = couponDto.IsLimited,
                    UsageLimit = couponDto.UsageLimit ?? 0,
                    AmountUses = couponDto.AmountUses ?? 0,
                    MinPurchaseAmount = couponDto.MinPurchaseAmount,
                    MaxPurchaseAmount = couponDto.MaxPurchaseAmount,
                    Status = couponDto.Status,
                    MarketingUserId = couponDto.MarketingUserId
                };

                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    _context.Coupons.Add(coupon);
                    await _context.SaveChangesAsync();
                    transaction.Complete();
                }

                return coupon;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the coupon. Please try again later.");
            }
        }

        // Asynchronous method to retrieve all coupons
        public async Task<ICollection<CouponEntity>> GetAllCoupons()
        {
            // Returns a list of all coupons from the database
            return await _context.Coupons.ToListAsync();
        }


    }

}
