namespace FoodCorner.Areas.Admin.ViewModels.Blog
{
    public class BlogTagViewModel
    {
        public BlogTagViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
