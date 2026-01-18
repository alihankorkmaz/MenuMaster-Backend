using MenuMaster.Database;
using MenuMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace MenuMaster.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<Reservation>> GetByUserIdAsync(int userId)
        {
            return await _context.Reservations
                .Include(r => r.Restaurant) // Artık hata vermemeli
                .Where(r => r.UserId == userId) // Eğer UserId modelde string ise toString yap
                .OrderByDescending(r => r.Date)
                .ToListAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return false;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Reservation> GetByIdAsync(int id)
        {
            // Veritabanından sadece o ID'ye sahip olan rezervasyonu getiriyoruz
            return await _context.Reservations.FindAsync(id);
        }
        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Reservation>> GetByRestaurantIdAsync(int restaurantId)
        {
            return await _context.Reservations
                .Include(r => r.User) // Müşteri bilgilerini çekmek için (Örn: r.User.Name)
                .Where(r => r.RestaurantId == restaurantId)
                .OrderByDescending(r => r.Date) // En yeni rezervasyon en üstte
                .ToListAsync();
        }
    }
}
