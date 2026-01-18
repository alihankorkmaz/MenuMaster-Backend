using MenuMaster.Dtos;
using MenuMaster.Models;
using System.Threading.Tasks;

namespace MenuMaster.Services
{
    public interface IReservationService
    {
        Task<bool> CreateReservationAsync(int userId, ReservationCreateDto dto);
        Task<List<ReservationListDto>> GetUserReservationsAsync(int userId);
        Task<bool> CancelReservationAsync(int reservationId, int userId);
        Task<bool> UpdateReservationStatusAsync(int id, string status);
        Task<List<ReservationListDto>> GetRestaurantReservationsAsync(int restaurantId);

    }
}
