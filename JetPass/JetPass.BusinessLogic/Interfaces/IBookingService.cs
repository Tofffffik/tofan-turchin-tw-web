using System.Collections.Generic;
using System.Threading.Tasks;
using JetPass.Core.DTOs;
using JetPass.Core.Entities;

namespace JetPass.BusinessLogic.Interfaces
{
    public interface IBookingService
    {
        Task<List<BookingDto>> GetBookings();
        Task<BookingEf> AddBooking(BookingDto flightDto);
    }
}