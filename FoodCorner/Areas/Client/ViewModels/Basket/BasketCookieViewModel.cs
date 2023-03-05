namespace FoodCorner.Areas.Client.ViewModels.Basket
{
    public class BasketCookieViewModel
    {
        public BasketCookieViewModel(int id, string? title, string? imageUrl, int quantity, decimal price, decimal total,int? sizeId , List<SizeListItemViewModel> sizes, int? personSize)
        {
            Id = id;
            Title = title;
            ImageUrl = imageUrl;
            Quantity = quantity;
            Price = price;
            Total = total;
            SizeId = sizeId;
            Sizes = sizes;
            PersonSize = personSize;
        }
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int DisCountPrice { get; set; }
        public decimal Total { get; set; }
        public List<SizeListItemViewModel> Sizes { get; set; }
        public int? SizeId { get; set; }
        public int? PersonSize { get; set; }


        public BasketCookieViewModel()
        {

        }
        public BasketCookieViewModel(int id, string? title, string? imageUrl, int quantity, decimal total, int disCountPrice,int? sizeId ,
            List<SizeListItemViewModel> sizes, 
            int? personSize)
        {
            Id = id;
            Title = title;
            ImageUrl = imageUrl;
            Quantity = quantity;
            DisCountPrice = disCountPrice;
            Total = total;
            SizeId = sizeId;
            Sizes = sizes;
            PersonSize=personSize;
        }
    }
}
