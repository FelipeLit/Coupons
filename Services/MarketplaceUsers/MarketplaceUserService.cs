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
        public async Task<ICollection<MarketplaceUserForUserDTO>> GetAllMarketplaceUsers()
        {
            var marketplaceUsers = await _context.MarketplaceUsers.ToListAsync();

            // Returns a list of all marketplaceUsers from the database
            return _mapper.Map<ICollection<MarketplaceUserForUserDTO>>(marketplaceUsers);
        }

        public async Task<MarketplaceUserForUserDTO> GetMarketplaceUserById(int id)
        {
            // Find the marketplaceUser by ID
            var marketplaceUsers = await _context.MarketplaceUsers.FindAsync(id);

            // Return the marketplaceUser entity user DTO.
            return _mapper.Map<MarketplaceUserForUserDTO>(marketplaceUsers);
        }

        public async Task<bool> UpdateMarketplaceUser(int id, MarketplaceUserForUserDTO marketplaceUserForUserDTO)
        {
            // Find the marketplaceUser by ID
            var marketplaceUserSearch = await _context.MarketplaceUsers.FindAsync(id);

            // If marketplaceUser not found, return false
            if (marketplaceUserSearch == null)
            {
                return false;
            }

            // Update marketplaceUser properties
            _mapper.Map(marketplaceUserForUserDTO, marketplaceUserSearch);
            
            // Save changes to the database
            await _context.SaveChangesAsync();
            return true;
        }
    }
}