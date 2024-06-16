using AutoMapper;
using Coupons.Data;
using Coupons.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Services.MarketingUsers
{
    public class MarketingUserService : IMarketingUserService
    {
        // Private variable to hold the database context
        private readonly CouponsContext _context;
        private readonly IMapper _mapper;

        // Constructor injecting the database context dependency
        public MarketingUserService(CouponsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ICollection<MarketingUserGetDTO>> GetAllMarketingUsers()
        {
            var marketingUsers = await _context.MarketingUsers.ToListAsync();

            // Returns a list of all marketingUsers from the database
            return _mapper.Map<ICollection<MarketingUserGetDTO>>(marketingUsers);
        }

        public async Task<MarketingUserGetDTO> GetMarketingUserById(int id)
        {
            // Find the marketingUser by ID
            var marketingUsers = await _context.MarketingUsers.FindAsync(id);

            // Return the marketingUser entity user DTO.
            return _mapper.Map<MarketingUserGetDTO>(marketingUsers);
        }

        public async Task<bool> UpdateMarketingUser(int id, MarketingUserPutDTO marketingUserPutDTO)
        {
            // Find the marketingUser by ID
            var marketingUserSearch = await _context.MarketingUsers.FindAsync(id);

            // If marketingUser not found, return false
            if (marketingUserSearch == null)
            {
                return false;
            }

            // Update marketingUser properties
            _mapper.Map(marketingUserPutDTO, marketingUserSearch);
            
            // Save changes to the database
            await _context.SaveChangesAsync();
            return true;
        }
    }
}