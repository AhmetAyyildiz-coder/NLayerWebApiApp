using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id); //primary key yaptık.
            builder.Property(x=>x.Id).UseIdentityColumn(); //default olarak 1 1 artar. 
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            //builder.ToTable("Category");//tablo ismini de bu şekilde değiştirebiliriz.
        }
    }
}
