namespace FoodCorner.Areas.Client.ViewModels.About
{
    public class StoryViewModel
    {
        public StoryViewModel(string contetn)
        {
            Contetn = contetn;
        }

        public string Contetn { get; set; }
    }
}
