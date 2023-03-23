namespace FoodCorner.Areas.Client.ViewModels.About
{
    public class VidioListViewModel
    {
        public VidioListViewModel(string vidioUrl)
        {
            VidioUrl = vidioUrl;
        }

        public string VidioUrl { get; set; }
    }
}
