using Microsoft.Build.Framework;

namespace FoodCorner.Areas.Admin.ViewModels.BlogCategory
{
    public class AddViewModel
    {
        [Required]
        public string Title { get; set; }
        public int? CategoryIds { get; set; }
        public List<CatagoryListItemViewModel>? Catagories { get; set; }



        public class CatagoryListItemViewModel
        {
            public int Id { get; set; }
            public string Title { get; set; }


            public CatagoryListItemViewModel(int id, string title)
            {
                Id = id;
                Title = title;
            }
        }

    }

}
