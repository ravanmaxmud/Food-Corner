namespace FoodCorner.Areas.Admin.ViewModels.Blog
{
    public class AddViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<int> TagIds { get; set; }
        public List<BlogCategoryViewModel>? Categorys { get; set; }
        public List<BlogTagViewModel>? Tags { get; set; }
    }
}
