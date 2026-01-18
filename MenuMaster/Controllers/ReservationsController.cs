using MenuMaster.Dtos;
using MenuMaster.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuMaster.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ReservationCreateDto dto)
        {
            // 1. Get the authenticated user's ID from the Token claims
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized(new { message = "User identification failed." });

            // 2. Save to database via the service layer
            var result = await _reservationService.CreateReservationAsync(userId, dto);

            if (result)
                return Ok(new { message = "Your reservation request has been successfully sent!" });

            return BadRequest(new { message = "Could not create reservation. Please try again." });
        }

        [HttpGet("my-reservations")]
        [Authorize]
        public async Task<IActionResult> GetMyReservations()
        {
            // Retrieve User ID from claims (Secure method)
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
                return Unauthorized(new { message = "Unauthorized access." });

            var result = await _reservationService.GetUserReservationsAsync(userId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> CancelReservation(int id)
        {
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized(new { message = "Authentication required." });

            var result = await _reservationService.CancelReservationAsync(id, userId);

            if (result)
                return Ok(new { message = "Reservation has been successfully cancelled." });

            return BadRequest(new { message = "Cancellation failed. You may not have permission." });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto dto)
        {
            // Update the reservation status via DTO
            var result = await _reservationService.UpdateReservationStatusAsync(id, dto.Status);

            if (result)
                return Ok(new { message = "Reservation status updated successfully!" });

            return NotFound(new { message = "Reservation not found." });
        }

        [Authorize]
        [HttpGet("by-restaurant")] 
        public async Task<IActionResult> GetRestaurantReservations()
        {
            // Get RestaurantId from Token claims
            var restaurantIdStr = User.Claims.FirstOrDefault(c => c.Type == "RestaurantId")?.Value;

            if (string.IsNullOrEmpty(restaurantIdStr))
                return Unauthorized(new { message = "Restaurant access required." });

            int restaurantId = int.Parse(restaurantIdStr);

            // Fetch results from the service
            var results = await _reservationService.GetRestaurantReservationsAsync(restaurantId);

            // Wrapped in an object as the JS frontend expects 'data.reservations'
            return Ok(new { reservations = results });
        }
    }
}