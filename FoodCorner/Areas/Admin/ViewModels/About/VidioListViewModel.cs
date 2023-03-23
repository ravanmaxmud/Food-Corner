namespace FoodCorner.Areas.Admin.ViewModels.About
{
    public class VidioListViewModel
    {
        public VidioListViewModel(int id, string vidioUrl)
        {
            Id = id;
            VidioUrl = vidioUrl;
        }

        public int Id { get; set; }
        public string VidioUrl { get; set; }
    }
}
