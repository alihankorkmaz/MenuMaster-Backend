using MenuMaster.Models;

namespace MenuMaster.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<Review>> GetByUserIdAsync(int userId);
        Task<bool> AddAsync(Review review);
        Task<bool> UpdateAsync(Review review);
        Task<bool> DeleteAsync(Review review);
        Task<Review?> GetByIdAsync(int id);

    }
}
