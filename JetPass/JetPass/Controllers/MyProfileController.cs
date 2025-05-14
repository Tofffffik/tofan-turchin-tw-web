using System.Threading.Tasks;
using System.Web.Mvc;
using JetPass.BusinessLogic.Interfaces;
using JetPass.BusinessLogic.Services;
using JetPass.Core.DTOs;

namespace JetPass.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly BookingService _bookingService = new BookingService();
        private readonly IUserService _userService = new UserService();

        public async Task<ActionResult> Index()
        {
            UserAndBookingsDto dto = new UserAndBookingsDto
            {
                Bookings = await _bookingService.GetBookings(),
                User = await _userService.GetUserById()
            };
            return View(dto);
        }
    }
}