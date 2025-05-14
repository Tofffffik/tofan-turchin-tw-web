using System.Threading.Tasks;
using System.Web.Mvc;
using JetPass.BusinessLogic.Services;

namespace JetPass.Controllers
{
    public class HomeController : Controller
    {
        private readonly FlightService _flightsRepository = new FlightService();
        public async Task<ActionResult> Index()
        {
            var flightDtos = await _flightsRepository.GetFlights(12);
            
            return View(flightDtos);
        }
    }
}