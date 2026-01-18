using MenuMaster.Models;
using MenuMaster.Repositories;
using MenuMaster.Services;
using Microsoft.AspNetCore.Identity;
public class PasswordResetService : IPasswordResetService
{
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepo;
    private readonly IRestaurantRepository _restaurantRepo;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IPasswordHasher<Restaurant> _restaurantPasswordHasher;

    public PasswordResetService(IEmailService emailService, IPasswordHasher<User> passwordHasher, IUserRepository userRepo, IRestaurantRepository restaurantRepo, IPasswordHasher<Restaurant> restaurantPasswordHasher)
    {
        _emailService = emailService;
        _passwordHasher = passwordHasher;
        _userRepo = userRepo;
        _restaurantRepo = restaurantRepo;
        _restaurantPasswordHasher = restaurantPasswordHasher;
    }

    public async Task<bool> RequestPasswordResetAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        email = email.Trim().ToLowerInvariant();

        // 1) check the user first
        var user = await _userRepo.GetByEmailAsync(email);
        if (user != null)
        {
            var code = OtpHelper.Generate6DigitCode();
            var codeHash = OtpHelper.HashCode(code);

            user.PasswordResetCodeHash = codeHash;
            user.PasswordResetCodeExpiresAt = DateTime.UtcNow.AddMinutes(10);
            user.PasswordResetCodeUsedAt = null;

            await _userRepo.UpdateAsync(user);

            return await _emailService.SendPasswordResetEmailAsync(
                user.Email,
                code,
                user.PasswordResetCodeExpiresAt.Value
            );
        }

        // 2) then check the restaurant
        var restaurant = await _restaurantRepo.GetRestaurantByEmailAsync(email);
        if (restaurant != null)
        {
            var code = OtpHelper.Generate6DigitCode();
            var codeHash = OtpHelper.HashCode(code);

            restaurant.PasswordResetCodeHash = codeHash;
            restaurant.PasswordResetCodeExpiresAt = DateTime.UtcNow.AddMinutes(10);
            restaurant.PasswordResetCodeUsedAt = null;

            await _restaurantRepo.UpdateRestaurantAsync(restaurant);

            return await _emailService.SendPasswordResetEmailAsync(
                restaurant.Email,
                code,
                restaurant.PasswordResetCodeExpiresAt.Value
            );
        }
        return false;
    }
    public async Task<(bool ok, string message)> ResetPasswordAsync(string email, string code, string newPassword)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(newPassword))
            return (false, "not valid!.");

        if (code.Length != 6)
            return (false, "Must be 6-digit code.");

        var incomingHash = OtpHelper.HashCode(code);

        // 1) check user first
        var user = await _userRepo.GetByEmailAsync(email);
        if (user != null)
        {
            var check = ValidateResetState(user.PasswordResetCodeHash, user.PasswordResetCodeExpiresAt, user.PasswordResetCodeUsedAt, incomingHash);
            if (!check.ok) return (false, check.message);

            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
            user.PasswordResetCodeUsedAt = DateTime.UtcNow;

            await _userRepo.UpdateAsync(user);
            return (true, "Your password was updated successfully!");
        }

        // 2) then check restaurant
        var restaurant = await _restaurantRepo.GetRestaurantByEmailAsync(email);
        if (restaurant != null)
        {
            var check = ValidateResetState(restaurant.PasswordResetCodeHash, restaurant.PasswordResetCodeExpiresAt, restaurant.PasswordResetCodeUsedAt, incomingHash);
            if (!check.ok) return (false, check.message);

            restaurant.PasswordHash = _restaurantPasswordHasher.HashPassword(restaurant, newPassword);
            restaurant.PasswordResetCodeUsedAt = DateTime.UtcNow;

            await _restaurantRepo.UpdateRestaurantAsync(restaurant);
            return (true, "Your password was updated successfully!");
        }

        return (false, "User could not found!");
    }

    private static (bool ok, string message) ValidateResetState(
        string? storedHash, DateTime? expiresAt, DateTime? usedAt, string incomingHash)
    {
        if (storedHash == null || expiresAt == null)
            return (false, "The password reset request not found.");

        if (usedAt != null)
            return (false, "The code has already been used.");

        if (expiresAt < DateTime.UtcNow)
            return (false, "The code has expired.");

        if (!string.Equals(storedHash, incomingHash, StringComparison.Ordinal))
            return (false, "Invalid code.");

        return (true, "");
    }
}