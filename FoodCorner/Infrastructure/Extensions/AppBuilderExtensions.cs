using FoodCorner.Areas.Admin.Hubs;
using FoodCorner.Areas.Client.Hubs;

namespace FoodCorner.Infrastructure.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void ConfigureMiddlewarePipeline(this WebApplication app)
        {
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=exists}/{controller=home}/{action=index}");

            app.MapHub<AlertHub>("hubs/alert-hub");
            app.MapHub<ChatHub>("hubs/chat-hub");
        }
    }
}
