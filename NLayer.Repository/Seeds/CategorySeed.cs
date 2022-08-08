using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;

namespace NLayer.Repository.Seeds
{
    internal class CategorySeed :IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {  /*
             * Seed yaparken asıl dikkat etmemiz gereken nokta id değerlerini manuel doldurmalıyız.
             */
            
            builder.HasData(new List<Category>
            {
          
                new Category()
                { //kırtasiye
                  Name = "stationary", Id = 1 
                },
                new Category()
                {
                    Name = "notebooks", Id = 2
                },
                new Category()
                {
                    Name = "Books", Id = 3
                }
            });
        }
    }
}
