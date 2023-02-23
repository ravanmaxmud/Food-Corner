namespace FoodCorner.Areas.Admin.ViewModels.SubNavbars
{
    public class ListItemViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string ToURL { get; set; }
        public int Order { get; set; }
        public string Navbar { get; set; }
        public ListItemViewModel(int id, string name, string toURL, int order, string navbar)
        {
            Id = id;
            Name = name;
            ToURL = toURL;
            Order = order;
            Navbar = navbar;
        }

    }
}
