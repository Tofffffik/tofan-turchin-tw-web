using System.Threading.Tasks;
using System.Web.Mvc;
using JetPass.BusinessLogic.Services;
using JetPass.Core.DTOs;

namespace JetPass.Controllers
{
    public class BookingsController: Controller
    {
        private readonly FlightService _flightService = new FlightService();
        private readonly BookingService _bookingService = new BookingService();
        
        [HttpGet]
        public async Task<ActionResult> Index(int id)
        {
            FlightAndBookingDto viewModel = new FlightAndBookingDto();
            var flightDto = await _flightService.GetFlight(id);
            viewModel.Flight = flightDto;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendData(FlightAndBookingDto model)
        {
            var response = await _bookingService.AddBooking(model.Booking);
            if(response!= null) 
                return RedirectToAction("Index", "MyProfile");
            return RedirectToAction("Index", "Home");
        }

    }
}