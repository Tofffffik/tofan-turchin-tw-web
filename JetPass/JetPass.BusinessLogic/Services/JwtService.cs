using System;
using System.IdentityModel.Tokens.Jwt;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JetPass.BusinessLogic.Infrastructure.Interfaces;
using JetPass.BusinessLogic.Infrastructure.Services;
using JetPass.BusinessLogic.Interfaces;
using JetPass.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using SecurityAlgorithms = Microsoft.IdentityModel.Tokens.SecurityAlgorithms;
using SecurityToken = Microsoft.IdentityModel.Tokens.SecurityToken;
using SecurityTokenDescriptor = Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor;
using SigningCredentials = Microsoft.IdentityModel.Tokens.SigningCredentials;
using SymmetricSecurityKey = Microsoft.IdentityModel.Tokens.SymmetricSecurityKey;

namespace JetPass.BusinessLogic.Services
{
    public class JwtService : IJwtService
    {
        private readonly ICookiesService _cookiesService;

        // Лучше использовать dependency injection
        public JwtService()
        {
            _cookiesService = new CookiesService();
        }

        public async Task<string> GenerateToken(UserEf user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var tokenHandler = new JwtSecurityTokenHandler();
            var keyHashing = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JwtSecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email), 
                }),
                
                Expires = DateTime.UtcNow.AddHours(1),
                
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyHashing),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<int> GetUserIdFromToken()
        {
            var token = await _cookiesService.GetCookie("jwt");

            if (string.IsNullOrEmpty(token))
                return 0;

            var idClaim = GetClaim("Id", token);

            return idClaim == null ? 0 : int.Parse(idClaim.Value);
        }

        public async Task<int> GetUserRoleIdFromToken()
        {
            var token = await _cookiesService.GetCookie("jwt");

            if (string.IsNullOrEmpty(token))
                return 0;

            var roleClaim = GetClaim("RoleId", token);

            return roleClaim == null ? 0 : int.Parse(roleClaim.Value);
        }

        public async Task<string> GetUserEmailFromToken()
        {
            var token = await _cookiesService.GetCookie("jwt");

            if (string.IsNullOrEmpty(token))
                return string.Empty;

            var emailClaim = GetClaim("Email", token);

            return emailClaim?.Value ?? string.Empty;
        }
        
        public Task<bool> IsTokenValid(string token)
        {
            if (string.IsNullOrEmpty(token))
                return Task.FromResult(false);
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyHashing = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JwtSecretKey"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyHashing),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true // Изменил на true для проверки срока действия
                }, out SecurityToken validatedToken);

                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public async Task<bool> IsTokenValid()
        {
            var cookie = await _cookiesService.GetCookie("Id");
                return !string.IsNullOrEmpty(cookie);
             // => await IsTokenValid(await _cookiesService.GetCookie("jwt"));
        }
        
        private Claim GetClaim(string claimType, string token)
        {
            if (string.IsNullOrEmpty(token)) 
                return null;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                return jwtToken.Claims.FirstOrDefault(claim => claim.Type == claimType);
            }
            catch
            {
                return null;
            }
        }
    }
}