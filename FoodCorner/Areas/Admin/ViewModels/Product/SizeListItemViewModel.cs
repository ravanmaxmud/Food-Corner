namespace FoodCorner.Areas.Admin.ViewModels.Product
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
