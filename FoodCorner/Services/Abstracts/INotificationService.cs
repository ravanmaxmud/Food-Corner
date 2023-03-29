namespace FoodCorner.Services.Abstracts
{
    public interface INotificationService
    {
            Task SendOrderCreatedToAdmin(string orderId);
    }
}
