using System.Collections.Generic;
using System.Threading.Tasks;
using JetPass.Core.DTOs;

namespace JetPass.BusinessLogic.Interfaces
{
    public interface IFlightService
    {
        Task<List<FlightDto>> GetFlights(int amount = 15);
        Task<FlightDto> GetFlight(int id);
        Task<FlightDto> AddFlight(FlightDto flightDto);
        Task<FlightDto> EditFlight(FlightDto flightDto);
        Task DeleteFlight(int id);
        Task<List<FlightDto>> GetSorted(FilterDto filterDto);
    }
}