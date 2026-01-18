namespace MenuMaster.Services
{
    public interface IEmailService
    {
        Task<bool> SendPasswordResetEmailAsync(string email, string resetCode, DateTime expiraton);
    }
}
