using FoodCorner.Database.Models.Enums;

namespace FoodCorner.Areas.Admin.ViewModels.Order
{
    public class ListItemViewModel
    {
        public string Id { get; set; }
        public DateTime CreatAt { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
		public ListItemViewModel(string id, DateTime creatAt, OrderStatus status, decimal total, string userEmail, string userAddress)
		{
			Id = id;
			CreatAt = creatAt;
			Status = status;
			Total = total;
			UserEmail = userEmail;
			UserAddress = userAddress;
		}

	}
}
