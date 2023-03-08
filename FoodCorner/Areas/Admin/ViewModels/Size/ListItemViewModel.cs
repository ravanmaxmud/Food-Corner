namespace FoodCorner.Areas.Admin.ViewModels.Size
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id,int personSize, int? increasePercent)
        {
            Id = id;
            PersonSize = personSize;
            IncreasePercent = increasePercent;
        }

        public int Id { get; set; }
        public int PersonSize { get; set; }
        public int? IncreasePercent { get; set; }
    }
}
