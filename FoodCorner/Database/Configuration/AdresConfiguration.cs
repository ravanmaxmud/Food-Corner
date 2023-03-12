using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FoodCorner.Database.Models;

namespace FoodCorner.Database.Configurations
{
    public class AdresConfiguration : IEntityTypeConfiguration<Addres>
    {
        public void Configure(EntityTypeBuilder<Addres> builder)
        {
            builder
               .ToTable("Address");
        }
    }
}
