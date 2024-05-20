namespace DataAccessLayer.Models
{
    public class FlightReservation : BaseEntity
    {
        public Guid IdAirline { get; set; }
        public Guid IdUser { get; set; }
        public string Chair { get; set; }
        public DateTime FlightDate { get; set; }
        public DateTime FlightDuration { get; set; }
        public string CheckInFlightLocation { get; set; }
        public string CheckOutFlightLocation { get; set; }
        public double Price {  get; set; }

        public FlightReservation() 
        {
            //Console.WriteLine("Chair: " + Chair);
        }

        //Ctor bun
        public FlightReservation(Guid IdAirline, Guid IdUser, string Chair, DateTime FlightDate, DateTime FlightDuration, string CheckInFlightLocation, string CheckOutFlightLocation, double Price) : base()
        {
            this.IdAirline = IdAirline;
            this.IdUser = IdUser;
            this.Chair = Chair;
            this.FlightDate = FlightDate;
            this.FlightDuration = FlightDuration;
            this.CheckInFlightLocation = CheckInFlightLocation;
            this.CheckOutFlightLocation = CheckOutFlightLocation;
            this.Price = Price;
        }

        //Ctor dummy
        public FlightReservation(string Chair, DateTime FlightDate, DateTime FlightDuration, string CheckInFlightLocation, string CheckOutFlightLocation, double Price) : base()
        {
            this.IdAirline = this.Id;
            this.IdUser = this.Id;
            this.Chair = Chair;
            this.FlightDate = FlightDate;
            this.FlightDuration = FlightDuration;
            this.CheckInFlightLocation = CheckInFlightLocation;
            this.CheckOutFlightLocation = CheckOutFlightLocation;
            this.Price = Price;
        }

        
    }
}
