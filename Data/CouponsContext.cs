using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Data
{
    public class CouponsContext : DbContext
    {
        public CouponsContext(DbContextOptions<CouponsContext>options) : base(options)
        {

        }

        
    }
}