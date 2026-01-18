namespace MenuMaster.Models;
public class PasswordResetToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public string UserEmail { get; set; }
    public DateTime ExpiryDate { get; set; }
}