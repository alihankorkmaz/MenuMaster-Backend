using MenuMaster.Services;
using Microsoft.AspNetCore.Mvc;
using MenuMaster.Dtos;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace MenuMaster.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ITokenService _tokenService;
        public RestaurantController(IRestaurantService restaurantService, ITokenService tokenService)
        {
            _restaurantService = restaurantService;
            _tokenService = tokenService;
        }
        //post api/restaurant/register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRestaurantDto registerRestaurantDto)
        {
            if (registerRestaurantDto == null)
                return BadRequest("Invalid data");
            if (string.IsNullOrEmpty(registerRestaurantDto.ImageUrl))
            {
                registerRestaurantDto.ImageUrl = "default_image_url.jpg";
            }               
            try
            {
                var restaurant = await _restaurantService.RegisterAsync(registerRestaurantDto);
                return Ok(new { Message = "Restaurant registered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            
        }
        //post api/restaurant/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var restaurant = await _restaurantService.LoginAsync(loginDto);
                var token = _tokenService.GenerateToken(restaurant);
                return Ok(new {
                    Message = "successfully login",
                    restaurantId = restaurant.Id,
                    name = restaurant.Name,
                    email = restaurant.Email,
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
        //put api_restaurant/update
        [HttpPut("update/{restaurantId}")]
        public async Task<IActionResult> UpdateRestaurant(int restaurantId, [FromBody] UpdateRestaurantDto updateRestaurantDto)
        {
            var updated = await _restaurantService.UpdateRestaurantAsync(restaurantId, updateRestaurantDto);

            if (updated == null)
                return NotFound(new { message = "Restaurant not found or update failed." });

            return Ok(new { message = "Restaurant updated successfully!" });
        }

        //get api/restaurants/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync();
            if (restaurants == null || restaurants.Count == 0)
            {
                return NotFound("No restaurants found");
            }
            return Ok(restaurants);
        }
        
        //get api/restaurant/{restaurantId}
        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetRestaurantById(int restaurantId)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
            if (restaurant == null)
                return NotFound("Restaurant not found");
            return Ok(restaurant);
        }
    }
}
