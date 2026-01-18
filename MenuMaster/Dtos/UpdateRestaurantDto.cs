using System.ComponentModel.DataAnnotations;

namespace MenuMaster.Dtos
{
    public class UpdateRestaurantDto
    {
        [StringLength(100, MinimumLength = 3)]
        public string? Name { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }

        [StringLength(80)]
        public string? City { get; set; }

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }

    }
}
