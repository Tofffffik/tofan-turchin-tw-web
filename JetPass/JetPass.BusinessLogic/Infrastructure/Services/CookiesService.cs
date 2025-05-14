using System;
using System.Threading.Tasks;
using System.Web;
using JetPass.BusinessLogic.Infrastructure.Interfaces;

namespace JetPass.BusinessLogic.Infrastructure.Services
{
    public class CookiesService : ICookiesService
    {
        public Task SetCookie(string key, string value, DateTime? expires = null)
        {
            var cookie = new HttpCookie(key)
            {
                HttpOnly = true,
                Secure = true,
                Expires = expires ?? DateTime.UtcNow.AddDays(30),
                Name = key,
                Value = value
            };
            
            HttpContext.Current.Response.Cookies.Add(cookie);
            return Task.CompletedTask;
        }

        public Task<string> GetCookie(string key)
        {
            var cookieData= HttpContext.Current.Request.Cookies[key]?.Value ?? string.Empty;
            return Task.FromResult(cookieData);
        }

        public Task RemoveCookie(string key)
        {
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                var cookie = new HttpCookie(key)
                {
                    Expires = DateTime.UtcNow.AddDays(-30)
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            return Task.CompletedTask;  
        }

        public Task<bool> IsCookieExists(string key) =>
          Task.FromResult(HttpContext.Current.Request.Cookies[key] != null);
    }
}