using AutoMapper;
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
        private readonly IMapper _mapper;

        // Constructor injecting the database context dependency
        public CouponService(CouponsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            catch (Exception)
            {
                throw new Exception("An error occurred while creating the coupon. Please try again later.");
            }
        }

        // Asynchronous method to retrieve all coupons
        public async Task<ICollection<CouponGetDTO>> GetAllCoupons()
        {
            var coupons = await _context.Coupons.ToListAsync();

            // Returns a list of all coupons from the database
            return _mapper.Map<ICollection<CouponGetDTO>>(coupons);
        }

        public async Task<CouponGetDTO> GetCouponById(int id)
        {
            // Find the coupon by ID
            var coupons = await _context.Coupons.FindAsync(id);

            // Return the coupon entity user DTO.
            return _mapper.Map<CouponGetDTO>(coupons);
        }

        public async Task<ICollection<CouponGetDTO>> GetCreatedCoupons(int marketplaceId)
        {
            // Return coupons whose creator has the provided ID
            var coupons = await _context.Coupons.Where(c => c.MarketingUserId == marketplaceId).ToListAsync() ?? throw new Exception("Cannot find coupon with ID: " + marketplaceId);   

            // Return coupons whose creator  
            return _mapper.Map<ICollection<CouponGetDTO>>(coupons);   
        }
        
        public async Task<bool> UpdateCoupon(int id, CouponGetDTO CouponGetDTO)
        {
            // Find the coupon by ID
            var couponSearch = await _context.Coupons.FindAsync(id);

            // If coupon not found, return false
            if (couponSearch == null)
            {
                return false;
            }
            var couponMarketingUserId = _context.MarketingUsers.Any(c => c.Id == CouponGetDTO.MarketingUserId);

            if (!couponMarketingUserId)
            {
               throw new Exception("The ID marketing user not found.");
            }

            // Update coupon properties
            _mapper.Map(CouponGetDTO, couponSearch);
            
            // Save changes to the database
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
