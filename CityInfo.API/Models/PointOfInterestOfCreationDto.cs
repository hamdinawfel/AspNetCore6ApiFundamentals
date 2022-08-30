using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestOfCreationDto
    {
        [Required(ErrorMessage = "Please Enter this field")]
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
