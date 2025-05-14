using System.Collections.Generic;
using System.Threading.Tasks;
using JetPass.Core.Entities;

namespace JetPass.Core.Interfaces.Users
{
    public interface IBookingsRepository : IGenericRepository<BookingEf>
    {
        Task<List<BookingEf>> GetAllAsync(int userId);
    }
}