using FoodCorner.Areas.Client.ActionFilter;
using FoodCorner.Services.Abstracts;
using FoodCorner.Services.Concretes;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Infrastructure.Configurations
{
    public static class RegisterCustomServicesConfigurations
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, SMTPService>();
            services.AddScoped<IUserActivationService, UserActivationService>();
            services.AddScoped<IBasketService, BasketService>();
            //services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ValidationCurrentUserAttribute>();
            services.AddScoped<IWishListService, WishListService>();
        }
    }
}
