using System.Threading.Tasks;
using System.Web.Mvc;
using JetPass.BusinessLogic.Interfaces;
using JetPass.BusinessLogic.Services;
using JetPass.Core.DTOs;

namespace JetPass.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService = new AuthenticationService();
        
        [HttpGet]
        public ActionResult SignIn() => View();

        [HttpGet]
        public ActionResult SignUp() => View();

        [HttpPost]
        public async Task<ActionResult> SignIn(SignInDto signInDto)
        {
            var response =await _authenticationService.SignIn(signInDto);
           
            if(response) 
                return RedirectToAction("Index", "Home");
            return View(signInDto);
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(SignUpDto signUpDto)
        {
            var response =await _authenticationService.SignUp(signUpDto);
            if(response)
                return RedirectToAction("Index", "Home");
            return View(signUpDto);
        }

        public ActionResult Logout()
        {
            _authenticationService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}