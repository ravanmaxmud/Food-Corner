namespace FoodCorner.Areas.Client.ViewModels.ContactUs
{
    public class AddressViewModel
    {
        public AddressViewModel(string city, string street, string phoneNumber, string email)
        {
            City = city;
            Street = street;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public string City { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
