namespace FoodCorner.Areas.Admin.ViewModels.Product
{
    public class ProductCommentList
    {
        public ProductCommentList(int id, string comment, string userFullName)
        {
            Id = id;
            Comment = comment;
            UserFullName = userFullName;
        }

        public int Id { get; set; }
        public string Comment { get; set; }
        public string UserFullName { get; set; }
    }
}
