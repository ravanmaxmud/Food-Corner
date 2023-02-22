using Microsoft.EntityFrameworkCore;
using FoodCorner.Extensions;
namespace FoodCorner.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
       : base(options)
        {

        }








        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly<Program>();
        }
    }
}
