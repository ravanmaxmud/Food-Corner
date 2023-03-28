namespace FoodCorner.Areas.Admin.ViewModels.Address
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string city, string street, string phoneNumber, string email)
        {
            Id = id;
            City = city;
            Street = street;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
