using MenuMaster.Models;

namespace MenuMaster.Repositories
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllByRestaurantIdAsync(int restaurantId);
        Task<Menu> GetByIdAsync(int id);
        Task AddAsync(Menu menu);
        Task DeleteAsync(Menu Menu);
        Task SaveChangesAsync();
    }
}
