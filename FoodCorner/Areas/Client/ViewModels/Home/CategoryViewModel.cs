namespace FoodCorner.Areas.Client.ViewModels.Home
{
    public class CategoryViewModel
    {
        public CategoryViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public CategoryViewModel(int id, string title, string backgroundİmageUrl)
        {
            Id = id;
            Title = title;
            BackgroundİmageUrl = backgroundİmageUrl;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string BackgroundİmageUrl { get; set; }
    }
}
