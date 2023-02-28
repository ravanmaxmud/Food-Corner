namespace FoodCorner.Areas.Client.ViewModels.Home
{
    public class ProductListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int? DiscountPercent { get; set; }
        public int? DiscountPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string MainImgUrl { get; set; }


        public ProductListItemViewModel(int id, string name, string description, int price, int? discountPercent ,int? discountPrice, DateTime createdAt, string mainImgUrl)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            DiscountPercent = discountPercent;
            DiscountPrice = discountPrice;
            CreatedAt = createdAt;
            MainImgUrl = mainImgUrl;
        }

        public ProductListItemViewModel()
        {

        }
    }
}
