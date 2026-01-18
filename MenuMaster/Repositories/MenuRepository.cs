using MenuMaster.Models;
using Microsoft.EntityFrameworkCore;
using MenuMaster.Database;
using System;

namespace MenuMaster.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _context;

        public MenuRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menu>> GetAllByRestaurantIdAsync(int restaurantId)
        {
            return await _context.Menus
                .Where(m => m.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<Menu> GetByIdAsync(int id)
        {
            return await _context.Menus.FindAsync(id);
        }

        public async Task AddAsync(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
        }

        public async Task DeleteAsync(Menu menu)
        {
            _context.Menus.Remove(menu);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
