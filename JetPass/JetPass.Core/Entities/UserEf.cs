using System.Collections.Generic;

namespace JetPass.Core.Entities
{
    public class UserEf
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool AgreeToTermsOfService { get; set; }
        public string HashPassword { get; set; }
        
        public List<BookingEf> Bookings { get; set; }
    }
}