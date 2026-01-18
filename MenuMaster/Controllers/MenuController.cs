using MenuMaster.Dtos;
using MenuMaster.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuMaster.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/restaurants")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("menu/{restaurantId?}")] // Optional parameter for flexibility
        [AllowAnonymous] // Allows public access (customers) to view the menu
        public async Task<IActionResult> GetMenu(int? restaurantId)
        {
            int finalId;

            if (restaurantId.HasValue)
            {
                // Scenario 1: Customer visits a restaurant page, ID comes from the URL.
                finalId = restaurantId.Value;
            }
            else
            {
                // Scenario 2: Restaurant owner in their dashboard, ID is retrieved from the token.
                var idClaim = User.Claims.FirstOrDefault(c => c.Type == "RestaurantId")?.Value;
                if (string.IsNullOrEmpty(idClaim))
                    return Unauthorized(new { message = "Please log in to access your dashboard." });

                finalId = int.Parse(idClaim);
            }

            var menuItems = await _menuService.GetMenuForRestaurantAsync(finalId);
            return Ok(new { menu = menuItems });
        }

        [HttpPost("menu")]
        public async Task<IActionResult> AddMenu([FromBody] MenuCreateDto dto)
        {
            var restaurantIdStr = User.Claims.FirstOrDefault(c => c.Type == "RestaurantId")?.Value;
            if (string.IsNullOrEmpty(restaurantIdStr))
            {
                return Unauthorized(new { message = "Invalid or missing token." });
            }

            int restaurantId = int.Parse(restaurantIdStr);
            await _menuService.CreateMenuItemAsync(restaurantId, dto);

            return Ok(new { message = $"Menu item successfully added to restaurant ID: {restaurantId}" });
        }

        [HttpDelete("menu/{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var restaurantIdClaim = User.Claims.FirstOrDefault(c => c.Type == "RestaurantId")?.Value;
            if (string.IsNullOrEmpty(restaurantIdClaim))
                return Unauthorized(new { message = "You are not authorized to perform this action." });

            int restaurantId = int.Parse(restaurantIdClaim);

            var result = await _menuService.DeleteMenuItemAsync(id, restaurantId);

            if (!result)
            {
                return NotFound(new { message = "Item not found or you do not have permission to delete it." });
            }

            return Ok(new { message = "Menu item successfully deleted." });
        }
    }
}