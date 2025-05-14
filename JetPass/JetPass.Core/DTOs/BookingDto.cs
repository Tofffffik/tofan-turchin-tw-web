using System;

namespace JetPass.Core.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PassportNumber { get; set; }
        public string Citizenship { get; set; }
        public int IdFlight { get; set; }
    }
}