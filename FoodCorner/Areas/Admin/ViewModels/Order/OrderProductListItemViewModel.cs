namespace FoodCorner.Areas.Admin.ViewModels.Order
{
    public class OrderProductListItemViewModel
    {
        public OrderProductListItemViewModel(int id, string imgURl, string productName, int personSize, int quantity, int total)
        {
            Id = id;
            ImgURl = imgURl;
            ProductName = productName;
            PersonSize = personSize;
            Quantity = quantity;
            Total = total;
        }

        public int Id { get; set; }
        public string ImgURl { get; set; }
        public string ProductName { get; set; }
        public int PersonSize { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
}
