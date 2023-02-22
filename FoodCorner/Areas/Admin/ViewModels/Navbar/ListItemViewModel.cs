namespace FoodCorner.Areas.Admin.ViewModels.Navbar
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ToURL { get; set; }
        public int Order { get; set; }
        public bool IsViewHeader { get; set; }
        public bool IsViewFooter { get; set; }

        public ListItemViewModel(int id, string name, string toURL, int order, bool isViewHeader, bool isViewFooter)
        {
            Id = id;
            Name = name;
            ToURL = toURL;
            Order = order;
            IsViewHeader = isViewHeader;
            IsViewFooter = isViewFooter;
        }
    }
}
