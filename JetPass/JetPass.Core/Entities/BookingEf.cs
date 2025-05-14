using System;
using JetPass.Core.Enums;

namespace JetPass.Core.Entities
{
    public class BookingEf
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CitizenShip { get; set; }
        public string PassportNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        
        public int FlightId { get; set; }
        public FlightEf Flight { get; set; }
        
        public int UserId { get; set; }
        public UserEf User { get; set; }
    }
}