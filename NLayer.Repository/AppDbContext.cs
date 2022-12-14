using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //cogul olarak modelconfigre dosyalarını dahil etme
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //tekil olarak modelbuilder dosyalarını dahil etme
            //modelBuilder.ApplyConfiguration(new Configurations.CategoryConfiguration);


            //dbcontext icerisnden seed yapma 
            modelBuilder.Entity<ProductFeature>().HasData(
                new ProductFeature
                {
                    Color = "Red",
                    Id = 1,
                    Height = 150,
                    ProductId = 1,
                    Width = 200
                });
        }
        
    }

    
}
