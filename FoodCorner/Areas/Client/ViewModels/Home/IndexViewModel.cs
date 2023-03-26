namespace FoodCorner.Areas.Client.ViewModels.Home
{
    public class IndexViewModel
    {
        public List<SliderViewModel> Sliders { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<StoryViewModel> Stories { get; set; }
        public List<FeedBackViewModel> FeedBacks { get; set; }
    }
}
