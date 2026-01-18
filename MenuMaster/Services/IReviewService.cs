using MenuMaster.Dtos;

namespace MenuMaster.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewViewDto>> GetRestaurantReviewsAsync(int restaurantId);
        Task<bool> AddReviewAsync(CreateReviewDto dto, int userId);
        Task<IEnumerable<MyReviewDto>> GetMyReviewsAsync(int userId);
        Task<bool> UpdateReviewAsync(int reviewId, UpdateReviewDto dto, int userId);
        Task<bool> DeleteReviewAsync(int reviewId, int userId);

    }
}
