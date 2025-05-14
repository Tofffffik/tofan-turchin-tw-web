using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using JetPass.Core.Entities;
using JetPass.Core.Interfaces.Users;
using JetPass.DAL;

namespace JetPass.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly JettContext _context;

        public UsersRepository()
        {
            _context = new JettContext();
        }

        public async Task<UserEf> GetByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserEf> CreateAsync(UserEf entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UserEf> UpdateAsync(UserEf entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserEf> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserEf> GetByNumberAsync(string number)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber  == number);
        }
    }
}