using System;
using System.Threading.Tasks;

namespace JetPass.BusinessLogic.Infrastructure.Interfaces
{
    public interface ICookiesService
    { 
        Task SetCookie(string key, string value, DateTime? expires = null);
        Task<string> GetCookie(string key);
        Task RemoveCookie(string key);
        Task<bool> IsCookieExists(string key);
    }
}