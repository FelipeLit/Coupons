using AutoMapper;
using Coupons.Data;
using Coupons.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Services.MarketplaceUsers
{
    public class MarketplaceUserService : IMarketplaceUserService
    {
        // Private variable to hold the database context
        private readonly CouponsContext _context;
        private readonly IMapper _mapper;

        // Constructor injecting the database context dependency
        public MarketplaceUserService(CouponsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ICollection<MarketplaceGetDTO>> GetAllMarketplaceUsers()
        {
            var marketplaceUsers = await _context.MarketplaceUsers.ToListAsync();

            // Returns a list of all marketplaceUsers from the database
            return _mapper.Map<ICollection<MarketplaceGetDTO>>(marketplaceUsers);
        }

        public async Task<MarketplaceGetDTO> GetMarketplaceUserById(int id)
        {
            // Find the marketplaceUser by ID
            var marketplaceUsers = await _context.MarketplaceUsers.FindAsync(id);

            // Return the marketplaceUser entity user DTO.
            return _mapper.Map<MarketplaceGetDTO>(marketplaceUsers);
        }

        public async Task<bool> UpdateMarketplaceUser(int id, MarketplaceGetDTO MarketplaceGetDTO)
        {
            // Find the marketplaceUser by ID
            var marketplaceUserSearch = await _context.MarketplaceUsers.FindAsync(id);

            // If marketplaceUser not found, return false
            if (marketplaceUserSearch == null)
            {
                return false;
            }

            // Update marketplaceUser properties
            _mapper.Map(MarketplaceGetDTO, marketplaceUserSearch);
            
            // Save changes to the database
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<MarketplaceUserGetCouponDTO>> GetUsersWithCoupons()
        {
            // Fetch users with their coupon usages from the database, including coupon details.
            var usersWithCoupons = await _context.MarketplaceUsers
                .Include(mu => mu.CouponUsages!)
                .ThenInclude(cu => cu.Coupon!)
                .ToListAsync();

            // Map the result to a collection of MarketplaceGetDTO and return it.
            return _mapper.Map<ICollection<MarketplaceUserGetCouponDTO>>(usersWithCoupons); 
        }
    }
}