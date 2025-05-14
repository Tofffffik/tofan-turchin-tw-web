using System.Threading.Tasks;
using JetPass.Core.DTOs;

namespace JetPass.BusinessLogic.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> SignUp(SignUpDto registrationDto);
        Task<bool> SignIn(SignInDto loginDto);
        Task Logout(); 
    }
}