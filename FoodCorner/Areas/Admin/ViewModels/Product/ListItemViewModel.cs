namespace FoodCorner.Areas.Admin.ViewModels.Product
{
    public class ListItemViewModel
    {
        public ListItemViewModel(string name, string description, int price, int id)
        {
            Name = name;
            Description = description;
            Price = price;
            Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
