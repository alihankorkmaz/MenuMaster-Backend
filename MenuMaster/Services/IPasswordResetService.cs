namespace MenuMaster.Services
{
    public interface IPasswordResetService
    {
        Task <bool> RequestPasswordResetAsync(string email);
        Task<(bool ok, string message)> ResetPasswordAsync(string email, string code, string newPassword);
    }
}
