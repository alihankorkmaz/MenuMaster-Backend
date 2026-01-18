using MenuMaster.Dtos;
using MenuMaster.Models;
using MenuMaster.Dtos;

namespace MenuMaster.Services
{
    public interface IRestaurantService
    {
        Task<Restaurant> RegisterAsync(RegisterRestaurantDto registerRestaurantDto);
        Task<Restaurant> LoginAsync(LoginDto loginDto);
        Task<Restaurant?> UpdateRestaurantAsync(int restaurantId, UpdateRestaurantDto updatedRestaurantDto);
        Task<bool?> DeleteRestaurantAsync(int restaurantId);
        Task<List<Restaurant>> GetAllRestaurantsAsync();
        Task<List<Restaurant>> GetRestaurantsAsync(string? city = null);
        Task<List<string>> GetCitiesAsync();
        Task<RestaurantInfoDto?> GetRestaurantByIdAsync(int restaurantId);
        Task<Restaurant?> FindRestaurantByEmailAsync(string email);
    }
}
