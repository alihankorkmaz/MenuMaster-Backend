using System.ComponentModel.DataAnnotations;

namespace MenuMaster.Dtos
{
    public class RegisterDto
    {
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }

        [StringLength(80)]
        public string City { get; set; }

        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
