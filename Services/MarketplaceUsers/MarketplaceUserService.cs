using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Coupons.Data;
using Coupons.Dto;
using Coupons.Models;
using Coupons.Utils;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Services.MarketplaceUsers
{
    public class MarketplaceUserService : IMarketplaceUserService
    {
        private readonly CouponsContext _context;
        private readonly IMapper _mapper;

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
        public async Task<MarketplaceUserEntity> ChangeStatus(int id)
        {
            try
            {
                var marketplaceUser = await _context.MarketplaceUsers.FindAsync(id);

                if (marketplaceUser == null)
                {
                    throw new ValidationException($"marketplaceUser with ID: {id} not found.");
                }

                if (marketplaceUser.Status == "Inactive")
                {
                    throw new ValidationException($"marketplaceUser with ID: {id} is already inactive.");
                }

                marketplaceUser.Status = "Inactive";
                _context.MarketplaceUsers.Update(marketplaceUser);
                await _context.SaveChangesAsync();

                return marketplaceUser;
            }
            catch (ValidationException)
            {
                throw;//majear las excepciones en el controlador
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while changing the status of the product. Please try again later." + ex.Message);
            }
        }

        public async Task<MarketplaceGetDTO> GetMarketplaceUserById(int id)
        {
            // Find the marketplaceUser by ID
            var marketplaceUsers = await _context.MarketplaceUsers.FindAsync(id);

            // Return the marketplaceUser entity user DTO.
            return _mapper.Map<MarketplaceGetDTO>(marketplaceUsers);
        }
        public async Task<MarketplaceUserEntity> CreateMarketplaceUser(MarketplaceUserDto marketplaceUserDto)
        {
            try
            {  
                var marketplaceUser = new MarketplaceUserEntity
                {
                    Username = marketplaceUserDto.Username,
                    Password = marketplaceUserDto.Password,
                    Email = marketplaceUserDto.Email,
                    Status = marketplaceUserDto.Status
                };

                var MarketingUserName = _context.MarketingUsers.FirstOrDefault(c => c.Id == 3);
                if (MarketingUserName == null)
                {
                    throw new ValidationException("The ID marketing user not found.");
                }

                _context.MarketplaceUsers.Add(marketplaceUser);
                await _context.SaveChangesAsync();

                var SendEmail = new MailersendUtils();

                if(SendEmail == null){
                    throw new ValidationException("Error al enviar el correo");
                }

                await SendEmail.EnviarCorreoUser
                ( 
                    marketplaceUserDto.Email,
                    marketplaceUserDto.Username,
                    MarketingUserName.Username
                );
                
                return marketplaceUser;
            }
            catch (Exception ex)
            {
                // Registrar detalles del error
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw new Exception("An error occurred while creating the marketplace user.", ex);
            }
        }


        public async Task<ICollection<MarketplacePutDTO>> GetAllMarketplaceRemove()
        {
            var marketplace = await _context.MarketplaceUsers.Where(p => p.Status == "Inactive").ToListAsync();

            return marketplace.Count != 0 ? _mapper.Map<ICollection<MarketplacePutDTO>>(marketplace) : [];
        }

        public async Task<MarketplaceUserEntity> RestoreStatus(int id)
        {
            try
            {
                var marketplace = await _context.MarketplaceUsers.FindAsync(id);

                if (marketplace == null)
                {
                    throw new ValidationException($"marketplace with ID: {id} not found.");
                }

                if (marketplace.Status == "Active")
                {
                    throw new ValidationException($"marketplace with ID: {id} is already active.");
                }

                marketplace.Status = "Active";
                _context.MarketplaceUsers.Update(marketplace);
                await _context.SaveChangesAsync();

                return marketplace;
            }
            catch (ValidationException)
            {
                throw;//majear las excepciones en el controlador
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while changing the status of the product. Please try again later." + ex.Message);
            }
        }

        public async Task<ICollection<MarketplaceUserGetCouponDTO>> GetUsersWithCoupons()
        {
            // Fetch users with their coupon usages from the database, including coupon details.
            var usersWithCoupons = await _context.MarketplaceUsers
                .Include(mu => mu.CouponUsages!)
                .ThenInclude(cu => cu.Coupon!)
                .ToListAsync();

            // Map the result to a collection of MarketplacePutDTO and return it.
            return _mapper.Map<ICollection<MarketplaceUserGetCouponDTO>>(usersWithCoupons);
        }

        public async Task<bool> UpdateMarketplaceUser(int id, MarketplacePutDTO MarketplacePutDTO)
        {

            var marketplaceUserSearch = await _context.MarketplaceUsers.FindAsync(id);

            // If marketplaceUser not found, return false
            if (marketplaceUserSearch == null)
            {
                return false;
            }

            // Update marketplaceUser properties
            _mapper.Map(MarketplacePutDTO, marketplaceUserSearch);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return true;
        }
    }
}