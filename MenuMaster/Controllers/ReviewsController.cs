using MenuMaster.Dtos;
using MenuMaster.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MenuMaster.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET api/reviews/restaurant/5
        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetReviews(int restaurantId)
        {
            var reviews = await _reviewService.GetRestaurantReviewsAsync(restaurantId);
            return Ok(new { reviews });
        }
        // GET api/reviews/my-reviews
        [HttpGet("my-reviews")]
        [Authorize]
        public async Task<IActionResult> GetMyReviews()
        {
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();
            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();

            var reviews = await _reviewService.GetMyReviewsAsync(userId);

            return Ok(reviews);
        }

        // POST api/reviews
        [HttpPost]
        [Authorize] // only authenticated users can add reviews
        public async Task<IActionResult> AddReview([FromBody] CreateReviewDto dto)
        {
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();
            var result = await _reviewService.AddReviewAsync(dto, userId);

            if (result) return Ok(new { message = "Review added successfully!" });
            return BadRequest(new { message = "Could not add review." });
        }
        // PUT api/reviews/5
        [HttpPut("{id}")]
        [Authorize] // only authenticated users can update reviews
        public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateReviewDto dto)
        {
            // Token'dan UserId'yi alıyoruz
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
                return Unauthorized();

            var result = await _reviewService.UpdateReviewAsync(id, dto, userId);

            if (result)
                return Ok(new { message = "Review updated successfully!" });

            return BadRequest(new { message = "Could not update review. Make sure it's your review." });
        }

        // DELETE api/reviews/5
        [HttpDelete("{id}")]
        [Authorize] // only authenticated users can delete reviews
        public async Task<IActionResult> DeleteReview(int id)
        {
            // Token'dan UserId'yi alıyoruz
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
                return Unauthorized();

            var result = await _reviewService.DeleteReviewAsync(id, userId);

            if (result)
                return Ok(new { message = "Review deleted successfully!" });

            return BadRequest(new { message = "Could not delete review. It might not exist or you are not authorized." });
        }
    }
}
