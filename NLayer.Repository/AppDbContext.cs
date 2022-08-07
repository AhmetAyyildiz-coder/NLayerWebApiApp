using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        public DbSet<NLayer.Core.Category> Categories { get; set; }
        public DbSet<NLayer.Core.Product> Products { get; set; }
        public DbSet<NLayer.Core.ProductFeature> ProductFeatures { get; set; }
        
    }
}
