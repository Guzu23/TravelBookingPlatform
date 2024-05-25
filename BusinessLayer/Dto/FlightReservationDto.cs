namespace BusinessLayer.Dto
{
    public class FlightReservationDto : BaseEntityDto
    {
        public Guid IdAirline { get; set; }
        public Guid IdUser { get; set; }
        public string Chair { get; set; }
        public DateTime FlightDate { get; set; }
        public DateTime FlightDuration { get; set; }
        public string CheckInFlightLocation { get; set; }
        public string CheckOutFlightLocation { get; set; }
        public double Price { get; set; }

        public FlightReservationDto() { }
    }
}
