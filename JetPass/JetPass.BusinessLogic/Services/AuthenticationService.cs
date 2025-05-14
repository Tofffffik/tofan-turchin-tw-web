    using System;
    using System.Threading.Tasks;
    using JetPass.BusinessLogic.Helper;
    using JetPass.BusinessLogic.Infrastructure.Services;
    using JetPass.BusinessLogic.Interfaces;
    using JetPass.Core.DTOs;
    using JetPass.Core.Entities;
    using JetPass.Core.Interfaces.Users;
    using JetPass.DAL.Repositories;

    namespace JetPass.BusinessLogic.Services
    {
        public class AuthenticationService : IAuthenticationService
        {
            private readonly CookiesService  _cookiesService = new CookiesService();
            private readonly IJwtService _jwtService = new JwtService();
            private readonly IUsersRepository _usersRepository = new UsersRepository();
            
            public async Task<bool> SignIn(SignInDto loginDto)
            {
                var user = await _usersRepository.GetByEmailAsync(loginDto.EmailOrPhoneNumber) 
                           ?? await _usersRepository.GetByNumberAsync(loginDto.EmailOrPhoneNumber);

                if (user == null || !Singleton<Hasher>.Instance.VerifyPassword(loginDto.Password, user.HashPassword))
                    return false;

                
                await _cookiesService.SetCookie("id_USER", user.Id.ToString(), DateTime.Now.AddHours(1));
                
                return true;
            }

            public async Task<bool> SignUp(SignUpDto registrationDto)
            {
                var emailExists = await _usersRepository.GetByEmailAsync(registrationDto.Email) != null;
                var phoneExists = await _usersRepository.GetByNumberAsync(registrationDto.PhoneNumber) != null;
                
                if (emailExists || phoneExists) return false;

                var userEf = new UserEf
                {
                    Name = registrationDto.Name,
                    Email = registrationDto.Email,
                    PhoneNumber = registrationDto.PhoneNumber,
                    HashPassword = Singleton<Hasher>.Instance.HashPassword(registrationDto.Password),
                    AgreeToTermsOfService = registrationDto.AgreeToTermsOfService
                };

                var created = await _usersRepository.CreateAsync(userEf);
                
                await _cookiesService.SetCookie("id_USER", created.Id.ToString(), DateTime.Now.AddHours(1));
                
                return true;
            }

            public async Task Logout()
            {
                await _cookiesService.RemoveCookie("id_USER");
            }
        }
    }