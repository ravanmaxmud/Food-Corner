using FoodCorner.Database;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Infrastructure.Configurations
{
    public static class DatabaseConfigurations
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("RevanPc"));
            });
        }
    }
}
