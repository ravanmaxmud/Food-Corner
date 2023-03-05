namespace FoodCorner.Areas.Client.ViewModels.Basket
{
    public class SizeListItemViewModel
    {
        public int Id { get; set; }
        public int PersonSize { get; set; }


        public SizeListItemViewModel(int id, int personSize)
        {
            Id = id;
            PersonSize = personSize;
        }
    }
}
