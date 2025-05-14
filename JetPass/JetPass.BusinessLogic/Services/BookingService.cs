using System.Collections.Generic;
using System.Threading.Tasks;
using JetPass.BusinessLogic.Infrastructure.Services;
using JetPass.BusinessLogic.Interfaces;
using JetPass.Core.DTOs;
using JetPass.Core.Entities;
using JetPass.Core.Interfaces.Users;
using JetPass.DAL.Repositories;
using System.Linq;

namespace JetPass.BusinessLogic.Services
{
    public class BookingService : IBookingService
    {
        private readonly CookiesService _cookiesService = new CookiesService();
        private readonly IBookingsRepository _bookingRepository = new BookingsRepository();

        public async Task<List<BookingDto>> GetBookings()
        {
            var userId = await _cookiesService.GetCookie("id_USER");
            int parsedUserId = int.Parse(userId);
            
            var bookings = await _bookingRepository.GetAllAsync(parsedUserId);
            var listBookings = bookings.Select(BookingEfToDto).ToList();
            return (await Task.WhenAll(listBookings)).ToList();
        }

        public async Task<BookingEf> AddBooking(BookingDto bookingDto)
        {
            var bookingEf = await BookingDtoToEf(bookingDto);
            var createdBooking = await _bookingRepository.CreateAsync(bookingEf);
            return createdBooking;
        }

        private Task<BookingDto> BookingEfToDto(BookingEf bookingEf)
        {
            return Task.FromResult(new BookingDto
            {
                Id = bookingEf.Id,
                Name = bookingEf.Name,
                LastName = bookingEf.Surname,
                Email = bookingEf.Email,
                Phone = bookingEf.PhoneNumber,
                PassportNumber = bookingEf.PassportNumber,
                IdFlight = bookingEf.FlightId,
                Citizenship = bookingEf.CitizenShip
            });
        }
        
        private async Task<BookingEf> BookingDtoToEf(BookingDto bookingDto)
        {
            var userId = await _cookiesService.GetCookie("id_USER");
            int parsedUserId = int.Parse(userId);
            
            return new BookingEf
            {
                Id = bookingDto.Id,
                Name = bookingDto.Name,
                Surname = bookingDto.LastName,
                Email = bookingDto.Email,
                PhoneNumber = bookingDto.Phone,
                PassportNumber = bookingDto.PassportNumber,
                CitizenShip = bookingDto.Citizenship,
                UserId = parsedUserId,
                FlightId = bookingDto.IdFlight,
            };
        }
    }
}