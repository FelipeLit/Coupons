using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coupons.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Data
{
    public class CouponsContext : DbContext
    {
        public CouponsContext(DbContextOptions<CouponsContext>options) : base(options)
        {

        }
        public DbSet<CouponEntity> Coupons { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CouponHistoryEntity> CouponHistory { get; set; }
        public DbSet<CouponUsageEntity> CouponUsages { get; set; }
        public DbSet<MarketingUserEntity> MarketingUsers { get; set; }
        public DbSet<MarketplaceUserEntity> MarketplaceUsers{ get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<PurchaseCouponEntity> PurchaseCoupon { get; set; }
        public DbSet<PurchaseEntity> Purchases { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserRoleEntity> UserRoles {get; set;}
    }
}