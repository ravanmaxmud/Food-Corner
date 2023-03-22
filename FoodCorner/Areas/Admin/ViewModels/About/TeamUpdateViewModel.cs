using System.ComponentModel.DataAnnotations;

namespace FoodCorner.Areas.Admin.ViewModels.About
{
    public class TeamUpdateViewModel
    {
        public int Id { get; set; }
        [Required]
		public string Name { get; set; }
		[Required]
		public string LastName { get; set; }

		public string? Instagram { get; set; }
		public string? LinkEdin { get; set; }
		public string? Facebook { get; set; }

		[Required]
		public IFormFile? MembersImage { get; set; }
		public string? MembersImageUrl { get; set; }
	}
}
