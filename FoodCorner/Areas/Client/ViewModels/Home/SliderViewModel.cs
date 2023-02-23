namespace FoodCorner.Areas.Client.ViewModels.Home
{
    public class SliderViewModel
    {
        public SliderViewModel(int id, string headerTitle, string mainTitle, string button, string buttonRedirectUrl, string backGroundImageUrl)
        {
            Id = id;
            HeaderTitle = headerTitle;
            MainTitle = mainTitle;
            Button = button;
            ButtonRedirectUrl = buttonRedirectUrl;
            BackGroundImageUrl = backGroundImageUrl;
        }

        public int Id { get; set; }
        public string HeaderTitle { get; set; }
        public string MainTitle { get; set; }
        public string Button { get; set; }
        public string ButtonRedirectUrl { get; set; }
        public string BackGroundImageUrl { get; set; }
        public int Order { get; set; }
        public SliderViewModel()
        {
        }
    }
}
