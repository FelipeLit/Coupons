using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Coupons.Data;
using Coupons.Dto;
using Coupons.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Services.MarketingUsers
{
    public class MarketingUserService : IMarketingUserService
    {
         private readonly CouponsContext _context;
         private readonly IMapper _mapper;
        public MarketingUserService(CouponsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<MarketingUserEntity> ChangeStatus(int id)
        {
            try
            {
                var marketinUser = await _context.MarketingUsers.FindAsync(id);

                if (marketinUser == null)
                {
                    throw new ValidationException($"marketinUser with ID: {id} not found.");
                }

                if (marketinUser.Status == "Inactive")
                {
                    throw new ValidationException($"marketinUser with ID: {id} is already inactive.");
                }

                marketinUser.Status = "Inactive";
                _context.MarketingUsers.Update(marketinUser);
                await _context.SaveChangesAsync();

                return marketinUser;
            }
            catch (ValidationException)
            {
                throw;//majear las excepciones en el controlador
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while changing the status of the marketingUser. Please try again later." + ex.Message);
            }
        }

        public async Task<MarketingUserEntity> CreateMarketingUser(MarketingUserDto marketingUserDto)
        {
            try
            {
                var marketingUser = new MarketingUserEntity
                {
                    Username = marketingUserDto.Username,
                    Password =  marketingUserDto.Password,
                    Email = marketingUserDto.Email,
                    Status = marketingUserDto.Status
                };
                _context.MarketingUsers.Add(marketingUser);
                await _context.SaveChangesAsync();


                return marketingUser;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the marketing user: " + ex.Message);
            }
        }

        public async Task<ICollection<MarketingUserGetDTO>> GetAllMarketingUserRemove()
        {
            var marketingUsers = await _context.MarketingUsers.Where(p => p.Status == "Inactive").ToListAsync();
            
            return marketingUsers.Count != 0 ? _mapper.Map<ICollection<MarketingUserGetDTO>>(marketingUsers) : [];
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

        public async Task<MarketingUserEntity> RestoreStatus(int id)
        {
             try
            {
                var marketingUser = await _context.MarketingUsers.FindAsync(id);

                if (marketingUser == null)
                {
                    throw new ValidationException($"marketingUser with ID: {id} not found.");
                }

                if (marketingUser.Status == "Active")
                {
                    throw new ValidationException($"marketingUser with ID: {id} is already active.");
                }

                marketingUser.Status = "Active";
                _context.MarketingUsers.Update(marketingUser);
                await _context.SaveChangesAsync();

                return marketingUser;
            }
            catch (ValidationException)
            {
                throw;//majear las excepciones en el controlador
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while changing the status of the marketing user. Please try again later." + ex.Message);
            }
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