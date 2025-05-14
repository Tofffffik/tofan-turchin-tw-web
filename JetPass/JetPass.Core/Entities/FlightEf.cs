using System;
using System.Collections;
using JetPass.Core.Enums;

namespace JetPass.Core.Entities
{
    public class FlightEf
    {
        public int Id { get; set; }
        public string AirLine { get; set; }
        public string AirCraft{get;set;}
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string FlightDescription { get; set; }
        public decimal Price { get; set; }
        public byte[] AirLineImage { get; set; }
        public FlightStatus Status { get; set; }
    }
}