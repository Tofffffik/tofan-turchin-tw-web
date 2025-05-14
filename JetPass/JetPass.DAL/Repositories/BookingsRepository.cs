using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using JetPass.Core.Entities;
using JetPass.Core.Interfaces.Users;
using JetPass.DAL;

namespace JetPass.DAL.Repositories
{
    public class BookingsRepository : IBookingsRepository
    {
        private readonly JettContext _context;

        public BookingsRepository()
        {
            _context = new JettContext();
        }

        public async Task<BookingEf> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Flight)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BookingEf> CreateAsync(BookingEf entity)
        {
            _context.Bookings.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<BookingEf> UpdateAsync(BookingEf entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var booking = await GetByIdAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<BookingEf>> GetAllAsync(int userId)
        {
            return _context.Bookings
                .Include(b => b.Flight)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
    }
}