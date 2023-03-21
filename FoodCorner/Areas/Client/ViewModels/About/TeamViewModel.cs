namespace FoodCorner.Areas.Client.ViewModels.About
{
    public class TeamViewModel
    {
        public TeamViewModel(string fullName, DateTime createdAt, string inistagram, string linkEdin, string faceBook, string memberImageUrl)
        {
            FullName = fullName;
            CreatedAt = createdAt;
            Inistagram = inistagram;
            LinkEdin = linkEdin;
            FaceBook = faceBook;
            MemberImageUrl = memberImageUrl;
        }

        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Inistagram { get; set; }
        public string LinkEdin { get; set; }
        public string FaceBook { get; set; }
        public string MemberImageUrl { get; set; }
    }
}
