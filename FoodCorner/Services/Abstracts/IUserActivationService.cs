using FoodCorner.Database.Models;

namespace FoodCorner.Services.Abstracts
{
    public interface IUserActivationService
    {
        Task SendActivationUrlAsync(User user);
    }
}
