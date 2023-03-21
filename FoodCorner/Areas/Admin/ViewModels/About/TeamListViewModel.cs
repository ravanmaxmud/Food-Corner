namespace FoodCorner.Areas.Admin.ViewModels.About
{
    public class TeamListViewModel
    {
        public TeamListViewModel(int id, string fullName, string inistagram, string linkEdin, string faceBook, string memberImageUrl)
        {
            Id = id;
            FullName = fullName;
            Inistagram = inistagram;
            LinkEdin = linkEdin;
            FaceBook = faceBook;
            MemberImageUrl = memberImageUrl;
        }

        public int Id { get; set; }
        public string FullName { get; set; }    
        public string Inistagram { get; set; }
        public string LinkEdin { get; set; }
        public string FaceBook { get; set; }
        public string MemberImageUrl { get; set; }
    }
}
