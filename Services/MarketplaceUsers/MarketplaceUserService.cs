using System.ComponentModel.DataAnnotations;
using Coupons.Data;
using Coupons.Dto;
using Coupons.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Services.MarketplaceUsers
{
    public class MarketplaceUserService : IMarketplaceUserService
    {
        private readonly CouponsContext _context;
        public MarketplaceUserService(CouponsContext context)
        {
            _context = context;
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

        public async Task<MarketplaceUserEntity> CreateMarketplaceUser(MarketplaceUserDto marketplaceUserDtoDto)
        {
            try
            {
                var marketplaceUser = new MarketplaceUserEntity
                {
                    Username = marketplaceUserDtoDto.Username,
                    Password =  marketplaceUserDtoDto.Password,
                    Email = marketplaceUserDtoDto.Email,
                    Status = marketplaceUserDtoDto.Status
                };
                _context.MarketplaceUsers.Add(marketplaceUser);
                await _context.SaveChangesAsync();


                return marketplaceUser;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the marketplace: " + ex.Message);
            }
        }

        public async Task<ICollection<MarketplaceUserEntity>> GetAllMarketplaceRemove()
        {
            var marketplace = await _context.MarketplaceUsers.Where(p => p.Status == "Inactive").ToListAsync();
            if (marketplace != null)
            {
                return marketplace;
            }
            else
            {
                return null;
            }
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
    }
}