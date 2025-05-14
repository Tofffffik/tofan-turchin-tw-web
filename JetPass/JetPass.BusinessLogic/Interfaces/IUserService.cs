using System.Threading.Tasks;
using JetPass.Core.DTOs;
using JetPass.Core.Entities;

namespace JetPass.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<SignUpDto> GetUserById();
    }
}