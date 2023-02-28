using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FoodCorner.Database.Models;

namespace FoodCorner.Database.Configuration
{

    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCatagory>
    {
        public void Configure(EntityTypeBuilder<ProductCatagory> builder)
        {
            builder
               .ToTable("ProductCategoryies");
        }
    }
}
