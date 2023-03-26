namespace FoodCorner.Areas.Admin.ViewModels.BlogTag
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
