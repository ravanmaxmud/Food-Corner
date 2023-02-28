using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FoodCorner.Database.Models;

namespace FoodCorner.Database.Configuration
{

    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder
               .ToTable("Sizes");
        }
    }
}
