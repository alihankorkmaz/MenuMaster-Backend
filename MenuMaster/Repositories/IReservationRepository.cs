using MenuMaster.Models;

namespace MenuMaster.Repositories
{
    public interface IReservationRepository
    {
        Task AddAsync(Reservation reservation);
        Task SaveChangesAsync();
        Task <List<Reservation>> GetByUserIdAsync(int userId);
        Task <bool> DeleteAsync(int id);
        Task<Reservation> GetByIdAsync(int id);
        Task UpdateAsync(Reservation reservation);
        Task<List<Reservation>> GetByRestaurantIdAsync(int restaurantId);
    }
}
