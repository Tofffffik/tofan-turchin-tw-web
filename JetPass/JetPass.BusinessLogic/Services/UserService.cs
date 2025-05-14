using System.Threading.Tasks;
using JetPass.BusinessLogic.Infrastructure.Services;
using JetPass.BusinessLogic.Interfaces;
using JetPass.Core.DTOs;
using JetPass.DAL.Repositories;

namespace JetPass.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly CookiesService _cookiesService = new CookiesService();
        private readonly UsersRepository _usersRepository = new UsersRepository();
        public async Task<SignUpDto> GetUserById()
        {
            var userId = await _cookiesService.GetCookie("id_USER");
            int parsedUserId = int.Parse(userId);
            
            var user = await _usersRepository.GetByIdAsync(parsedUserId);
            return new SignUpDto
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

        }
    }
}