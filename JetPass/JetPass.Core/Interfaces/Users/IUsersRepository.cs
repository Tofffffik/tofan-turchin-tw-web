using System.Threading.Tasks;
using JetPass.Core.Entities;

namespace JetPass.Core.Interfaces.Users
{
    public interface IUsersRepository : IGenericRepository<UserEf>
    {
        Task<UserEf> GetByEmailAsync(string email);
        Task<UserEf> GetByNumberAsync(string number);
    }
}