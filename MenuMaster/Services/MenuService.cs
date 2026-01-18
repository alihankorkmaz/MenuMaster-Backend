using MenuMaster.Dtos;
using MenuMaster.Models;
using MenuMaster.Repositories;

namespace MenuMaster.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repository;

        public MenuService(IMenuRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MenuListItemDto>> GetMenuForRestaurantAsync(int restaurantId)
        {
            var items = await _repository.GetAllByRestaurantIdAsync(restaurantId);
            // Burada AutoMapper kullanabilirsin veya manuel map'leyebilirsin
            return items.Select(i => new MenuListItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                Category = i.Category,
                Description = i.Description
            });
        }

        public async Task CreateMenuItemAsync(int restaurantId, MenuCreateDto dto)
        {
            var menu = new Menu
            {
                RestaurantId = restaurantId,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Category = dto.Category,
                CreatedAt = DateTime.Now
            };
            await _repository.AddAsync(menu);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteMenuItemAsync(int id, int restaurantId)
        {
            // Hem ürün ID'si hem de Restoran ID'si eşleşen ürünü bul
            var menuItem = await _repository.GetByIdAsync(id);

            // Ürün yoksa veya ürünün sahibi bu restoran değilse hata döndür
            if (menuItem == null || menuItem.RestaurantId != restaurantId)
            {
                return false;
            }

            await _repository.DeleteAsync(menuItem);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
