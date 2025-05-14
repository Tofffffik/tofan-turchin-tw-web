using System.Collections.Generic;

namespace JetPass.Core.DTOs
{
    public class UserAndBookingsDto
    {
        public SignUpDto User { get; set; } 
        public List<BookingDto> Bookings { get; set; } 
    }
}