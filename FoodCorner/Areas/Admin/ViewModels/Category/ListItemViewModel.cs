namespace FoodCorner.Areas.Admin.ViewModels.Category
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string title, string parentName)
        {
            Id = id;
            Title = title;
            ParentName = parentName;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ParentName { get; set; }

    }
}
