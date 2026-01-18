using MenuMaster.Dtos;
using MenuMaster.Models;
using MenuMaster.Repositories;

namespace MenuMaster.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<ReviewViewDto>> GetRestaurantReviewsAsync(int restaurantId)
        {
            var reviews = await _reviewRepository.GetByRestaurantIdAsync(restaurantId);

            return reviews.Select(r => new ReviewViewDto
            {
                Id = r.Id,
                UserId = r.UserId,
                UserName = r.User != null ? r.User.Name : "Anonymous", // if User is null, show "Anonymous"
                Rating = r.Rating,
                Comment = r.Comment,
                Date = r.CreatedAt
            });
        }
        public async Task<IEnumerable<MyReviewDto>> GetMyReviewsAsync(int userId)
        {
            var reviews = await _reviewRepository.GetByUserIdAsync(userId);

            return reviews.Select(r => new MyReviewDto
            {
                Id = r.Id,
                RestaurantId = r.RestaurantId,
                RestaurantName = r.Restaurant != null ? r.Restaurant.Name : "Unknown",
                Rating = r.Rating,
                Comment = r.Comment,
                Date = r.CreatedAt
            });
        }

        public async Task<bool> AddReviewAsync(CreateReviewDto dto, int userId)
        {
            var existingReviews = await _reviewRepository.GetByUserIdAsync(userId);
            var alreadyReviewed = existingReviews.Any(r => r.RestaurantId == dto.RestaurantId);

            if (alreadyReviewed)
            {
                // the user has already reviewed this restaurant
                return false;
            }

            var review = new Review
            {
                RestaurantId = dto.RestaurantId,
                UserId = userId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.Now
            };

            return await _reviewRepository.AddAsync(review);
        }
        public async Task<bool> UpdateReviewAsync(int reviewId, UpdateReviewDto dto, int userId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review == null) return false;
            if (review.UserId != userId) return false;

            review.Rating = dto.Rating;
            review.Comment = dto.Comment;

            return await _reviewRepository.UpdateAsync(review);
        }
        public async Task<bool> DeleteReviewAsync(int reviewId, int userId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review == null) return false;
            if (review.UserId != userId) return false;

            return await _reviewRepository.DeleteAsync(review);
        }


    }
}

