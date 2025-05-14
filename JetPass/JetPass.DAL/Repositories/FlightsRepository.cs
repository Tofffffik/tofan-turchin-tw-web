using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using JetPass.Core.Entities;
using JetPass.Core.Interfaces.Users;
using JetPass.DAL;

namespace JetPass.DAL.Repositories
{
    public class FlightsRepository : IFlightsRepository
    {
        private readonly JettContext _context;

        public FlightsRepository()
        {
            _context = new JettContext();
        }

        public async Task<FlightEf> GetByIdAsync(int id)
        {
            return await _context.Flights
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<FlightEf> CreateAsync(FlightEf entity)
        {
            _context.Flights.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FlightEf> UpdateAsync(FlightEf entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var flight = await GetByIdAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<FlightEf>> GetAllFlights(int amount)
        {
             var ien = _context.Flights.Take(amount);
             return ien.ToListAsync();
        }
    }
}