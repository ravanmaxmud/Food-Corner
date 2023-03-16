namespace FoodCorner.Areas.Client.ViewModels.Shop
{
	public class TagViewModel
	{
		public TagViewModel(int id, string title)
		{
			Id = id;
			Title = title;
		}

		public int Id { get; set; }
        public string Title { get; set; }
    }
}
