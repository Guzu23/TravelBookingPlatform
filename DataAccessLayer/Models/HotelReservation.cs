namespace DataAccessLayer.Models
{
    public class HotelReservation : BaseEntity
    {
        public Guid IdHotel {  get; set; }
        public Guid IdUser {  get; set; }
        public string RoomType { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public double Price {  get; set; }


        public HotelReservation() { }
        //Ctor ok
        public HotelReservation(Guid IdHotel, Guid IdUser, DateTime CheckIn, DateTime CheckOut, double Price, string RoomType) : base()
        {
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
            this.RoomType = RoomType;
            this.Price = Price;
            this.IdHotel = IdHotel;
            this.IdUser = IdUser;
        }

        //Ctor dummy
        public HotelReservation(DateTime CheckIn, DateTime CheckOut, double Price, string RoomType) : base()
        {
            this.CheckIn = CheckIn;
            this.CheckOut = CheckOut;
            this.RoomType = RoomType;
            this.Price = Price;
            this.IdUser = this.Id;
            this.IdHotel = this.Id;
        }
    }
}
