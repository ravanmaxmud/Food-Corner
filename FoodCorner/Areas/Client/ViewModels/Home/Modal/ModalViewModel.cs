namespace FoodCorner.Areas.Client.ViewModels.Home.Modal
{
    public class ModalViewModel
    {
        public ModalViewModel(int id, string title, string description, int price, List<Images> imgUrl, List<SizeViewModeL> sizes)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            ImgUrl = imgUrl;
            Sizes = sizes;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public List<Images> ImgUrl { get; set; }
        public List<SizeViewModeL> Sizes { get; set; }







        public class Images 
        {
            public Images(int id, string imageUrl)
            {
                Id = id;
                ImageUrl = imageUrl;
            }

            public int Id { get; set; }
            public string ImageUrl { get; set; }
        }

        public class SizeViewModeL
        {
            public SizeViewModeL(int title, int id)
            {
                Title = title;
                Id = id;
            }

            public int Id { get; set; }
            public int Title { get; set; }
        }
    }
}
