using System.Threading.Tasks;
using JetPass.Core.Entities;

namespace JetPass.BusinessLogic.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateToken(UserEf user);
        Task<int> GetUserIdFromToken();
        Task<int> GetUserRoleIdFromToken();
        Task<string> GetUserEmailFromToken();
        Task<bool> IsTokenValid();
    }
}