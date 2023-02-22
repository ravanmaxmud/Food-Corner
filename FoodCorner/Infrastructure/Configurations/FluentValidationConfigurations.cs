using FoodCorner.Database;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Infrastructure.Configurations
{
    public static class FluentValidationConfigurations
    {
        public static void ConfigureFluentValidatios(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<Program>();
        }
    }
}
