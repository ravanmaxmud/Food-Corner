using FoodCorner.Contracts.Email;

namespace FoodCorner.Services.Abstracts
{
    public interface IEmailService
    {
        public void Send(MessageDto messageDto);
    }
}
