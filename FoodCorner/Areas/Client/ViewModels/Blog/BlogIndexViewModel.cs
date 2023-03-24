namespace FoodCorner.Areas.Client.ViewModels.Blog
{
    public class BlogIndexViewModel
    {

        public BlogIndexViewModel(List<CategoryListItemViewModel> categories, List<TagListItemViewModel> tags)
        {
            Categories = categories;
            Tags = tags;
        }

        public List<CategoryListItemViewModel> Categories { get; set; }

        public List<TagListItemViewModel> Tags { get; set; }


    }

    public class CategoryListItemViewModel
    {
        public CategoryListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class TagListItemViewModel
    {
        public TagListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}

