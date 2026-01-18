using System.ComponentModel.DataAnnotations;

namespace MenuMaster.Dtos
{
    public class RegisterRestaurantDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]  
        public string Name { get; set; }

        [Required]
        [EmailAddress]  
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)] 
        public string Password { get; set; }

        [Required]
        [Phone]  
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(250)]  
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, StringLength(80)]
        public string City { get; set; }

        public string? ImageUrl { get; set; }  
    }

}
