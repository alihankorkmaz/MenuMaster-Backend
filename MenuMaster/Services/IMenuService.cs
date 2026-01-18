using MenuMaster.Dtos;

namespace MenuMaster.Services
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuListItemDto>> GetMenuForRestaurantAsync(int restaurantId);
        Task CreateMenuItemAsync(int restaurantId, MenuCreateDto dto);
        Task<bool> DeleteMenuItemAsync(int id, int restaurantId);
    }
}
