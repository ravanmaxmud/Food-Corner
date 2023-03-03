namespace FoodCorner.Areas.Admin.ViewModels.Story
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string content)
        {
            Id = id;
            Content = content;
        }

        public int Id { get; set; }
        public string Content { get; set; }
    }
}
