using AutoMapper;
using Coupons.Data;
using Coupons.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Services.Roles
{
    public class RoleService : IRoleService
    {
        // Private variable to hold the database context
        private readonly CouponsContext _context;
        private readonly IMapper _mapper;

        // Constructor injecting the database context dependency
        public RoleService(CouponsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<RoleGetDTO>> GetAllRoles()
        {
            // Get all roles from the database
            var roles = await _context.Roles
                            .Include(u => u.UserRoles!)   
                            .ThenInclude(r => r.MarketingUser)
                            .ToListAsync();

            // Returns a list of all roles from the database
            return _mapper.Map<ICollection<RoleGetDTO>>(roles);
        }

        public async Task<ICollection<RoleGetDTO>> GetRoleById(int id)
        {
            // Find the role by ID
            var roles = await _context.Roles
                                .Include(u => u.UserRoles!)   
                                .ThenInclude(r => r.MarketingUser)
                                .Where(r => r.Id == id)
                                .ToListAsync();

            // Returns a list of all roles from the database
            return _mapper.Map<ICollection<RoleGetDTO>>(roles);
        }

        public async Task AssignRoleToUser(UserRolePostDTO request)
        {
            var userRole = await _context.UserRoles.AnyAsync(r => r.MarketingUserId == request.MarketingUserId && r.RoleId == request.RoleId);;

            if(userRole)
            {
                throw new Exception("This marketing user already exists with the same role in the database.");
            }

            var marketing = await _context.MarketingUsers.FirstOrDefaultAsync(mk => mk.Id == request.MarketingUserId) ?? throw new Exception("Cannot find marketing user with ID: " + request.MarketingUserId);
            if (marketing.Status == "Inactive")
            {
                throw new Exception("This marketing user cannot be assigned a role, as the marketing user is inactive");
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId) ?? throw new Exception("Cannot find role with ID: " + request.RoleId);
            if (role.Status == "Inactive")
            {
                throw new Exception($"This role cannot be assigned to a marketing user, as the role is inactive");
            }
            
            // Map the UserRolePostDTO to the UserRole entity
            var userRoleEntity = _mapper.Map<UserRoleEntity>(request);

            // Add the UserRole entity to the context
            _context.UserRoles.Add(userRoleEntity);

            // Save the changes to the database
            await _context.SaveChangesAsync();
        }

        public async Task DenyAssignRoleToUser(UserRolePostDTO request)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(r => r.MarketingUserId == request.MarketingUserId && r.RoleId == request.RoleId);;

            if(userRole == null)
            {
                throw new Exception("There is no marketing user with this role.");
            }
            var marketing = await _context.MarketingUsers.AnyAsync(mk => mk.Id == request.MarketingUserId);

            if (!marketing)
            {
                throw new Exception("Cannot find marketing user with ID: " + request.MarketingUserId);
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId) ?? throw new Exception("Cannot find role with ID: " + request.RoleId);


            // Add the UserRole entity to the context
            _context.UserRoles.Remove(userRole);

            // Save the changes to the database
            await _context.SaveChangesAsync();
        }

        public async Task<RoleEntity> CreateRole(RolePostDTO rolePostDTO)
        {
            try
            {
                var Role = new RoleEntity
                {
                    Name = rolePostDTO.Name,
                    Status = rolePostDTO.Status,
          
                };

                
                _context.Roles.Add(Role);
                await _context.SaveChangesAsync();


                return Role;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the product: " + ex.Message);
            }
        }
    }
}