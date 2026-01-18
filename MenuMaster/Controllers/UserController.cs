using Microsoft.AspNetCore.Mvc;
using MenuMaster.Services;
using MenuMaster.Dtos;
using Microsoft.Extensions.Logging;

namespace MenuMaster.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordResetService _passwordResetService;
        private readonly ILogger<UserController> _logger;
        private readonly ITokenService _tokenService;
        public UserController(IUserService userService, IPasswordResetService passwordResetService, ILogger<UserController> logger, ITokenService tokenService)
        {
            _userService = userService;
            _passwordResetService = passwordResetService;
            _logger = logger;
            _tokenService = tokenService;
        }

        // POST api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (registerDto == null)
                return BadRequest("Invalid data");

            try
            {
                var user = await _userService.RegisterAsync(registerDto);
                return Ok(new { Message = "User registered successfully"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // POST api/user/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var user = await _userService.LoginAsync(loginDto);
                var token = _tokenService.GenerateToken(user);
                return Ok(new { 
                    Message = "successfully login",
                    userId = user.Id,
                    name = user.Name,
                    username = user.Username,
                    email = user.Email,
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
        // PUT api/user/update
        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserDto updatedUserDto)
        {
           var updatedUser = await _userService.UpdateUserAsync(userId, updatedUserDto);
            if (updatedUser == null)
            {
                return NotFound("User not found");
            }
            return Ok(updatedUser);
        }
        // DELETE api/user/{userId}
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _userService.DeleteUserAsync(userId);
            if (!result) return NotFound("User not found");
            
            return Ok("User deleted successfully");
        }

        // GET api/user/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var userInfo = await _userService.GetUserByIdAsync(userId);
            if (userInfo == null)
            {
                return NotFound("User not found");
            }
            return Ok(userInfo);
        }

    }
}
