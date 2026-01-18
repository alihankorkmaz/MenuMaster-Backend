namespace MenuMaster.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? PasswordResetCodeHash { get; set; }
        public DateTime? PasswordResetCodeExpiresAt { get; set; }
        public DateTime? PasswordResetCodeUsedAt { get; set; }
    }
}
