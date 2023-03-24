namespace FoodCorner.Areas.Admin.ViewModels.Blog
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string title, string content, List<TagViewModel> tags, List<CategoryViewModeL> categories, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Content = content;
            Tags = tags;
            Categories = categories;
            CreatedAt = createdAt;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public List<CategoryViewModeL> Categories { get; set; }



        public class TagViewModel
        {
            public TagViewModel(string title)
            {
                Title = title;
            }

            public string Title { get; set; }
        }
        public class CategoryViewModeL
        {
            public CategoryViewModeL(string title, string parentTitle)
            {
                Title = title;
                ParentTitle = parentTitle;
            }

            public string Title { get; set; }
            public string ParentTitle { get; set; }


        }
    }
}
