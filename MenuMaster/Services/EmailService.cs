using System.Net;
using System.Net.Mail;
using MenuMaster.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class EmailService : IEmailService
{
    private readonly bool _enabled;
    private readonly string? _smtpServer;
    private readonly string? _fromEmail;
    private readonly string? _fromPassword;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _logger = logger;
        _enabled = configuration.GetValue<bool>("Email:Enabled");

        _smtpServer = configuration["Email:SmtpServer"];
        _fromEmail = configuration["Email:FromEmail"];
        _fromPassword = configuration["Email:FromPassword"];
    }

    public async Task<bool> SendPasswordResetEmailAsync(string email, string resetCode, DateTime expiration)
    {
        if (!_enabled)
        {
            // demo mode - log the email instead of sending
            _logger.LogInformation("Email sending is disabled. Would send reset code {ResetCode} to {Email}.", resetCode, email);
            return true;
        }

        if (string.IsNullOrWhiteSpace(_smtpServer) ||
            string.IsNullOrWhiteSpace(_fromEmail) ||
            string.IsNullOrWhiteSpace(_fromPassword))
        {
            _logger.LogWarning("Email config is missing. Check Email:SmtpServer, Email:FromEmail, Email:FromPassword.");
            return false;
        }

        string htmlBody = $@"
        <div style='background-color: #f0f4f8; padding: 50px 20px; font-family: -apple-system, BlinkMacSystemFont, ""Segoe UI"", Roboto, Helvetica, Arial, sans-serif;'>
            <div style='max-width: 500px; margin: 0 auto; background-color: #ffffff; border-radius: 16px; padding: 40px; box-shadow: 0 4px 15px rgba(0,0,0,0.05);'>
                <div style='margin-bottom: 30px;'>
                    <span style='font-size: 24px; font-weight: 800; color: #1A1A1A;'>Menu<span style='color: #FF6B00;'>Master</span></span>
                </div>
                <div style='margin-bottom: 30px;'>
                    <h1 style='font-size: 48px; font-weight: 700; color: #FF6B00; margin: 0; letter-spacing: -1px;'>{resetCode}</h1>
                </div>
                <div style='color: #4A4A4A; font-size: 16px; line-height: 1.6;'>
                    <p style='margin-bottom: 20px; font-weight: 500;'>This reset code is valid for 1 hour.</p>
                    <p style='margin-bottom: 20px;'>You are receiving this email because a password reset request was made for your account.</p>
                    <p style='margin-top: 30px; font-size: 14px; color: #888;'>If you did not request this, please ignore this email; your password will remain unchanged.</p>
                </div>
            </div>
            <div style='text-align: center; margin-top: 20px; color: #888; font-size: 12px;'>
                © {DateTime.Now.Year} MenuMaster. All rights reserved.
            </div>
        </div>";

        using var message = new MailMessage(_fromEmail, email)
        {
            Subject = $"{resetCode} is your MenuMaster recovery code",
            Body = htmlBody,
            IsBodyHtml = true
        };

        using var smtpClient = new SmtpClient(_smtpServer)
        {
            Port = 587,
            Credentials = new NetworkCredential(_fromEmail, _fromPassword),
            EnableSsl = true
        };

        try
        {
            await smtpClient.SendMailAsync(message);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send password reset email to {Email}.", email);
            return false;
        }
    }
}
