using System.Collections.Generic;
using System.Threading.Tasks;
using JetPass.Core.Entities;

namespace JetPass.Core.Interfaces.Users
{
    public interface IFlightsRepository : IGenericRepository<FlightEf>
    {
        Task<List<FlightEf>> GetAllFlights(int amount);
    }
}