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

        public async Task<CouponEntity> ChangeStatus(int id)
        {
            try
            {
                var coupon = await _context.Coupons.FindAsync(id);

                if (coupon == null)
                {
                    throw new ValidationException($"Coupon with ID: {id} not found.");
                }

                if (coupon.Status == "Inactive")
                {
                    throw new ValidationException($"Coupon with ID: {id} is already inactive.");
                }

                coupon.Status = "Inactive";
                _context.Coupons.Update(coupon);
                await _context.SaveChangesAsync();

                return coupon;
            }
            catch (ValidationException)
            {
                throw;//majear las excepciones en el controlador
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while changing the status of the coupon. Please try again later." + ex.Message);
            }
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
        public async Task<ICollection<CouponForUserDTO>> GetAllCoupons()
        {
            var coupons = await _context.Coupons.ToListAsync();

            // Returns a list of all coupons from the database
            return _mapper.Map<ICollection<CouponForUserDTO>>(coupons);
        }

        public async Task<ICollection<CouponEntityUserDTO>> GetAllCouponsRemove()
        {
            var coupons = await _context.Coupons.Where(c=>c.Status == "Inactive").ToListAsync();

            // Returns a list of all coupons from the database
            return _mapper.Map<ICollection<CouponEntityUserDTO>>(coupons);
        }

        public async Task<ICollection<CouponEntityUserDTO>> GetAllCouponsRemove()
        {
            var coupons = await _context.Coupons.Where(c=>c.Status == "Inactive").ToListAsync();

            // Returns a list of all coupons from the database
            return _mapper.Map<ICollection<CouponEntityUserDTO>>(coupons);
        }

        public async Task<CouponForUserDTO> GetCouponById(int id)
        {
            var coupons = await _context.Coupons.FindAsync(id);

            return _mapper.Map<CouponEntityUserDTO>(coupons);
        }

        public async Task<CouponEntity> RestoreStatus(int id)
        {
           try
            {
                var coupon = await _context.Coupons.FindAsync(id);

                if (coupon == null)
                {
                    throw new ValidationException($"Coupon with ID: {id} not found.");
                }

                if (coupon.Status == "Active")
                {
                    throw new ValidationException($"Coupon with ID: {id} is already active.");
                }

                coupon.Status = "Active";
                _context.Coupons.Update(coupon);
                await _context.SaveChangesAsync();

                return coupon;
            }
            catch (ValidationException)
            {
                throw;//majear las excepciones en el controlador
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while changing the status of the coupon. Please try again later." + ex.Message);
            }
        }
    }

}
