using FoodCorner.Database.Models.Common;

namespace FoodCorner.Database.Models
{
    public class AboutVidio : BaseEntity,IAuditable
    {
        public string Vidio { get; set; }
        public string VidoInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
