using System.Data.Entity;
using JetPass.Core.Entities;
using System.Collections.Generic;
using System;
using JetPass.Core.DTOs;
using JetPass.Core.Enums;
using System.Linq;

namespace JetPass.DAL
{
    public class JettContext : DbContext
    {
        public DbSet<UserEf> Users { get; set; }
        public DbSet<FlightEf> Flights { get; set; }
        public DbSet<BookingEf> Bookings { get; set; }

        public JettContext() : base("DefaultConnection")
        {
            // Проверяем и заполняем рейсы при создании контекста
            FillFlightsIfNeeded();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEf>().HasKey(u => u.Id);
            modelBuilder.Entity<UserEf>().HasMany(u => u.Bookings).WithRequired(b => b.User).HasForeignKey(b => b.UserId).WillCascadeOnDelete(true);
            modelBuilder.Entity<BookingEf>().HasKey(b => b.Id);
            modelBuilder.Entity<BookingEf>().HasRequired(b => b.Flight).WithMany().HasForeignKey(b => b.FlightId).WillCascadeOnDelete(false);
            modelBuilder.Entity<FlightEf>().HasKey(f => f.Id);
            
            base.OnModelCreating(modelBuilder);
        }

        private void FillFlightsIfNeeded()
        {
            if (!Flights.Any() || Flights.Count() < 100)
            {
                var neededFlights = 100 - (Flights.Any() ? Flights.Count() : 0);
                var flightsToAdd = GenerateSampleFlights(neededFlights, Flights.Any() ? Flights.Max(f => f.Id) + 1 : 1);

                Flights.AddRange(flightsToAdd);
                SaveChanges();
            }
        }

        private List<FlightEf> GenerateSampleFlights(int count, int startId)
        {
            var flights = new List<FlightEf>();
            var random = new Random();
            var airlines = new[] { "SkyJet", "AeroFlot", "Emirates", "Lufthansa", "Qatar Airways" };
            var aircrafts = new[] { "Boeing 737", "Airbus A320", "Boeing 787", "Airbus A350", "Embraer E190" };
            var airports = new[] { "JFK", "LAX", "SFO", "ORD", "DFW", "MIA", "SEA", "ATL", "DXB", "LHR" };

            for (int i = 0; i < count; i++)
            {
                var departureTime = DateTime.Now.AddHours(random.Next(1, 24)).AddDays(random.Next(1, 30));
                var arrivalTime = departureTime.AddHours(random.Next(2, 12));

                string departureAirport, arrivalAirport;
                do
                {
                    departureAirport = airports[random.Next(airports.Length)];
                    arrivalAirport = airports[random.Next(airports.Length)];
                } while (departureAirport == arrivalAirport);

                flights.Add(new FlightEf
                {
                    Id = startId + i,
                    AirLine = airlines[random.Next(airlines.Length)],
                    AirCraft = aircrafts[random.Next(aircrafts.Length)],
                    FlightNumber = $"{random.Next(100, 999)}{(char)random.Next('A', 'Z')}",
                    DepartureAirport = departureAirport,
                    ArrivalAirport = arrivalAirport,
                    DepartureTime = departureTime,
                    ArrivalTime = arrivalTime,
                    FlightDescription = $"Flight from {departureAirport} to {arrivalAirport}",
                    Price = random.Next(100, 1000),
                    AirLineImage = null,
                    Status = (FlightStatus)random.Next(0, 8)
                });
            }

            return flights;
        }
    }
}   