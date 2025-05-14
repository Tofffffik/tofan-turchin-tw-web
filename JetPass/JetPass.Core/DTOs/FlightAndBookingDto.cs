namespace JetPass.Core.DTOs
{
    public class FlightAndBookingDto
    {
        public FlightDto Flight { get; set; } = new FlightDto();
        public BookingDto Booking { get; set; } = new BookingDto();
    }
}