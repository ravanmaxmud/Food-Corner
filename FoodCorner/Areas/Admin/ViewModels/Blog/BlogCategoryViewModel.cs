namespace FoodCorner.Areas.Admin.ViewModels.Blog
{
    public class BlogCategoryViewModel
    {
        public BlogCategoryViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
