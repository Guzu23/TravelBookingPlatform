namespace BusinessLayer.Dto
{
    public class HotelReservationDto : BaseEntityDto
    {
        public Guid IdHotel { get; set; }
        public Guid IdUser { get; set; }
        public string RoomType { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public double Price { get; set; }

        public HotelReservationDto() { }
    }
}
