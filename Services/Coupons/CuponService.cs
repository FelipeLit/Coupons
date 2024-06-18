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

        public async Task<ICollection<CouponsDto>> GetAllCouponsRemove()
        {
            var coupons = await _context.Coupons.Where(c=>c.Status == "Inactive").ToListAsync();

            // Returns a list of all coupons from the database
            return _mapper.Map<ICollection<CouponsDto>>(coupons);
        }

public async Task<ICollection<PurchaseCouponEntity>> GetAllCouponsPurchased()
        {
    try
    {
        var couponPurchases = await _context.PurchaseCoupon
            .Include(pc => pc.Coupon)
            .Include(pc => pc.Purchase)
            .Select(pc => new
            {
                pc.Id,
                pc.CouponId,
                pc.PurchaseId,
                Purchase = new
                {
                    pc.Purchase.Id,
                    pc.Purchase.Date,
                    pc.Purchase.Amount,
                    pc.Purchase.Discount,
                    pc.Purchase.Total,
                    // Asignar valores por defecto para MarketplaceUserId y ProductId
                    MarketplaceUserId = pc.Purchase.MarketplaceUserId,
                    ProductId = pc.Purchase.ProductId
                }
            })
            .ToListAsync();

        var result = couponPurchases.Select(cp => new PurchaseCouponEntity
        {
            Id = cp.Id,
            CouponId = cp.CouponId,
            PurchaseId = cp.PurchaseId,
            Purchase = new PurchaseEntity
            {
                Id = cp.Purchase.Id,
                Date = cp.Purchase.Date,
                Amount = cp.Purchase.Amount,
                Discount = cp.Purchase.Discount,
                Total = cp.Purchase.Total,
                // Asignar los valores de MarketplaceUserId y ProductId
                MarketplaceUserId = cp.Purchase.MarketplaceUserId,
                ProductId = cp.Purchase.ProductId
            }
        }).ToList();

        return result;
    }
    catch (ValidationException)
    {
        throw; // Manejar las excepciones en el controlador
    }
    catch (Exception ex)
    {
        throw new Exception("An error occurred while retrieving the purchased coupons. Please try again later.");
    }
}




       

        public async Task<CouponForUserDTO> GetCouponById(int id)
        {
            var coupons = await _context.Coupons.FindAsync(id);

            return _mapper.Map<CouponForUserDTO>(coupons);
        }

        public async Task<ICollection<MarketplaceUserForUserDTO>> GetUsersWithCouponsAsync()
        {
            // Fetch users with their coupon usages from the database, including coupon details.
            var usersWithCoupons = await _context.MarketplaceUsers
                .Include(mu => mu.CouponUsages!)
                .ThenInclude(cu => cu.Coupon!)
                .ToListAsync();

            // Map the result to a collection of MarketplaceUserForUserDTO and return it.
            return _mapper.Map<ICollection<MarketplaceUserForUserDTO>>(usersWithCoupons); 
        }

        public async Task<ICollection<CouponForUserDTO>> GetCreatedCoupons(int marketplaceId)
        {
            // Return coupons whose creator has the provided ID
            var coupons = await _context.Coupons.Where(c => c.MarketingUserId == marketplaceId).ToListAsync() ?? throw new Exception("Cannot find coupon with ID: " + marketplaceId);   

            // Return coupons whose creator  
            return _mapper.Map<ICollection<CouponForUserDTO>>(coupons);   
        }
        
        public async Task<bool> UpdateCoupon(int id, CouponForUserDTO couponForUserDTO)
        {
            // Find the coupon by ID
            var couponSearch = await _context.Coupons.FindAsync(id);

            // If coupon not found, return false
            if (couponSearch == null)
            {
                return false;
            }
            var couponMarketingUserId = _context.MarketingUsers.Any(c => c.Id == couponForUserDTO.MarketingUserId);

            if (!couponMarketingUserId)
            {
               throw new Exception("The ID marketing user not found.");
            }

            // Update coupon properties
            _mapper.Map(couponForUserDTO, couponSearch);
            
            // Save changes to the database
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<CouponEntity> RestoreStatus(int id)
        {
            throw new NotImplementedException();
        }

        Task<ICollection<MarketplaceUserForUserDTO>> ICouponService.GetUsersWithCouponsAsync()
        {
            throw new NotImplementedException();
        }
    }

}
