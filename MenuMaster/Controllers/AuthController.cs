using MenuMaster.Dtos;
using MenuMaster.Services;
using Microsoft.AspNetCore.Mvc;

namespace MenuMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IPasswordResetService _passwordResetService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IPasswordResetService passwordResetService, ILogger<AuthController> logger)
        {
            _passwordResetService = passwordResetService;
            _logger = logger;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] PasswordResetRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BadRequest(new { message = "Please provide a valid email address." });

            _logger.LogInformation($"Password reset requested for {request.Email}");

            var result = await _passwordResetService.RequestPasswordResetAsync(request.Email);

            if (result)
                return Ok(new { message = "Password reset code has been sent to your email." });

            return NotFound(new { message = "Email address not found." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var (isSuccess, message) = await _passwordResetService.ResetPasswordAsync(dto.Email, dto.Code, dto.NewPassword);

            if (isSuccess)
                return Ok(new { message });

            return BadRequest(new { message });
        }
    }
}