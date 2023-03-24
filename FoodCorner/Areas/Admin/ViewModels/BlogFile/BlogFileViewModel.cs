namespace FoodCorner.Areas.Admin.ViewModels.BlogFile
{
    public class BlogFileViewModel
    {
        public int BlogId { get; set; }
        public List<ListItem> Files { get; set; }

        public class ListItem 
        {

            public int Id { get; set; }
            public string FileUrl { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}
