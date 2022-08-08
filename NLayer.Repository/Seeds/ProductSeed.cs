using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product()
            {
                Id = 1,
                CategoryId = 1,
                Price = 200,
                Stock = 50
                ,Name="Pencil 1 "
            }
            ,
            new Product()
            {
                Id = 2,
                CategoryId = 1,
                Price = 250,
                Stock = 40
                ,
                Name = "Pencil 2 "
            },
            new Product()
            {
                Id = 3,
                CategoryId = 1,
                Price = 300,
                Stock = 20
                ,
                Name = "Pencil 3 "
            },
            new Product()
            {
                Id = 4,
                CategoryId = 2,
                Price = 160,
                Stock = 20
                ,
                Name = "HomeWork Books"
            });
        }
    }
}
