using System.Threading.Tasks;
using System.Web.Mvc;
using JetPass.BusinessLogic.Services;
using JetPass.Core.DTOs;

namespace JetPass.Controllers
{
    public class FlightsController : Controller
    {
        private readonly FlightService _flightService = new FlightService();
        
        [HttpGet]
        public ActionResult Index() => View();

        [HttpGet]
        public async Task<ActionResult> Flight(int id)
        {
            var flightDto = await _flightService.GetFlight(id);

            return View(flightDto);
        }
        
        [HttpGet]
        public async Task<ActionResult> GetFlight(int id)
        {
            var flightDto = await _flightService.GetFlight(id);
            
            return Json(new {success =true, flightDto});
        }

        [HttpGet]
        public async Task<JsonResult> GetSortedFlights(FilterDto filter)
        {
            var list = await _flightService.GetSorted(filter);
            return Json(new
            {
                success = true,
                flights = list,
                totalCount = list.Count
            }, JsonRequestBehavior.AllowGet);
        }
    }
}