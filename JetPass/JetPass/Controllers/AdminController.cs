using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using JetPass.BusinessLogic.Services;
using JetPass.Core.DTOs;
using JetPass.Core.Enums;

namespace JetPass.Controllers
{
    public class AdminController : Controller
    {
        // In-memory storage for demo purposes (replace with database in production)
        private static List<FlightDto> _flights = new List<FlightDto>
        {
            new FlightDto
            {
                Id = 1,
                AirLine = "Turkish Airlines",
                AirCraft = "Airbus A321",
                FlightNumber = "TK-415",
                DepartureAirport = "VKO",
                ArrivalAirport = "IST",
                DepartureTime = DateTime.Parse("2023-06-15 06:45:00"),
                ArrivalTime = DateTime.Parse("2023-06-15 08:00:00"),
                FlightDescription = "Direct flight",
                Price = 15000,
                Status = FlightStatus.OnSchedule
            }
        };

        private readonly FlightService _flightService = new FlightService();
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var list =await _flightService.GetFlights(200);
            return View(list);
        }

        [HttpPost]
        public async Task<ActionResult> CreateFlight(FlightDto flightDto)
        {
            var result = await _flightService.AddFlight(flightDto);
            if (result != null)
                return Json(new { success = true, flight = result });
            return Json(new { success = false, message = "Failed to create flight" });
        }

        [HttpPost]
        public async Task<ActionResult> EditFlight(FlightDto flightDto)
        {
            var result = await _flightService.EditFlight(flightDto);
            if (result != null)
                return Json(new { success = true, flight = result });
            return Json(new { success = false, message = "Failed to edit flight" });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteFlight(int id)
        { 
            await _flightService.DeleteFlight(id);
            return Json(new { success = true});
        }
        
        [HttpGet]
        public async Task<ActionResult> GetFlight(int id)
        {
            var result = await _flightService.GetFlight(id);
            if (result != null)
                return Json(new { success = true, flight = result }, JsonRequestBehavior.AllowGet);
            return Json(new { success = false, message = "Flight not found" }, JsonRequestBehavior.AllowGet);
        }
    }
}