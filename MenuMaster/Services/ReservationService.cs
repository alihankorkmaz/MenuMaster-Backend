using MenuMaster.Dtos;
using MenuMaster.Models;
using MenuMaster.Repositories;
namespace MenuMaster.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public async Task<bool> CreateReservationAsync(int userId, ReservationCreateDto dto)
        {
            var fullDateTime = DateTime.Parse($"{dto.Date} {dto.Time}");

            var reservation = new Reservation
            {
                UserId = userId,
                RestaurantId = dto.RestaurantId,
                People = dto.People,
                Date = fullDateTime,
                Status = "Pending" // default status
            };

            await _reservationRepository.AddAsync(reservation);
            await _reservationRepository.SaveChangesAsync();
            return true;
        }
        public async Task<List<ReservationListDto>> GetUserReservationsAsync(int userId)
        {
            var reservations = await _reservationRepository.GetByUserIdAsync(userId);

            return reservations.Select(r => new ReservationListDto
            {
                Id = r.Id,
                RestaurantName = r.Restaurant?.Name ?? "unkown restaurant",
                Date = r.Date,
                PeopleCount = r.People,
                Status = r.Status
            }).ToList();
        }
        public async Task<bool> CancelReservationAsync(int reservationId, int userId)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId);

            if (reservation == null || reservation.UserId != userId)
                return false;

            return await _reservationRepository.DeleteAsync(reservationId);
        }
        public async Task<bool> UpdateReservationStatusAsync(int id, string status)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);

            if (reservation == null) return false;

            reservation.Status = status;

            await _reservationRepository.UpdateAsync(reservation);
            return true;
        }
        public async Task<List<ReservationListDto>> GetRestaurantReservationsAsync(int restaurantId)
        {
            var reservations = await _reservationRepository.GetByRestaurantIdAsync(restaurantId);

            // filtering and mapping to DTO
            return reservations.Select(r => new ReservationListDto
            {
                Id = r.Id,
                UserName = r.User?.Name ?? "Guest",
                PeopleCount = r.People,
                Date = r.Date,
                Status = r.Status
            }).ToList();
        }
    }
}
