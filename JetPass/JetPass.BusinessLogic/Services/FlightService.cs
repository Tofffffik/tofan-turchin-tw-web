using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetPass.BusinessLogic.Interfaces;
using JetPass.Core.DTOs;
using JetPass.Core.Entities;
using JetPass.Core.Interfaces.Users;
using JetPass.DAL.Repositories;

namespace JetPass.BusinessLogic.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightsRepository _flightsRepository = new FlightsRepository();
        
        
        public async Task<List<FlightDto>> GetFlights(int amount = 15)
        {
            var result = await _flightsRepository.GetAllFlights(amount);
            var tasks = result.Select(ConvertEfToDto).ToList();
            return (await Task.WhenAll(tasks)).ToList();
        }

        public async Task<FlightDto> GetFlight(int id)
        {
            var flightEf = await _flightsRepository.GetByIdAsync(id);
            return await ConvertEfToDto(flightEf);
        }

        public async Task<FlightDto> AddFlight(FlightDto flightDto)
        {
            var flightEf = ConvertDtoToEf(flightDto);
            var createdFlight = await _flightsRepository.CreateAsync(flightEf);
            return await ConvertEfToDto(createdFlight);
        }

        public async Task<FlightDto> EditFlight(FlightDto flightDto)
        {
            var flightEf = ConvertDtoToEf(flightDto);
            var updatedFlight = await _flightsRepository.UpdateAsync(flightEf);
            return await ConvertEfToDto(updatedFlight);
        }

        public async Task DeleteFlight(int id)
        {
            await _flightsRepository.DeleteByIdAsync(id);
        }

        public async Task<List<FlightDto>> GetSorted(FilterDto filterDto)
        {
            var list = await GetFlights(500);
            // Сортировка по цене и фильтрация по аэропортам прибытия и вылета
            var filteredList = list.Where(f =>
                    (string.IsNullOrEmpty(filterDto.From) || f.DepartureAirport.Contains(filterDto.From)) &&
                    (string.IsNullOrEmpty(filterDto.To) || f.ArrivalAirport.Contains(filterDto.To)) &&
                    f.Price >= filterDto.MinPrice &&
                    f.Price <= filterDto.MaxPrice)
                .ToList();
            
            var listSoartedByPrice = filteredList.OrderBy(f => f.Price).ToList();
            return listSoartedByPrice;
        }

        private FlightEf ConvertDtoToEf(FlightDto flightDto)
        {
            return new FlightEf
            {
                Id = flightDto.Id,
                AirLine = flightDto.AirLine,
                AirCraft = flightDto.AirCraft,
                FlightNumber = flightDto.FlightNumber,
                DepartureAirport = flightDto.DepartureAirport,
                ArrivalAirport = flightDto.ArrivalAirport,
                DepartureTime = flightDto.DepartureTime,
                ArrivalTime = flightDto.ArrivalTime,
                FlightDescription = flightDto.FlightDescription,
                Price = flightDto.Price,
                AirLineImage = flightDto.AirLineImage,
                Status = flightDto.Status
            };
        }
        
        private async Task<FlightDto> ConvertEfToDto(FlightEf flightEf)
        {
            return await Task.FromResult(new FlightDto
            {
                Id = flightEf.Id,
                AirLine = flightEf.AirLine,
                AirCraft = flightEf.AirCraft,
                FlightNumber = flightEf.FlightNumber,
                DepartureAirport = flightEf.DepartureAirport,
                ArrivalAirport = flightEf.ArrivalAirport,
                DepartureTime = flightEf.DepartureTime,
                ArrivalTime = flightEf.ArrivalTime,
                FlightDescription = flightEf.FlightDescription,
                Price = flightEf.Price,
                AirLineImage = flightEf.AirLineImage,
                Status = flightEf.Status
            });
        }
    }
}