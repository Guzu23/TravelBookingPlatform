using DataAccessLayer.Models;

namespace BusinessLayer.Contracts
{
    public interface ILinqHotelReservation
    {
        List<HotelReservation> GetReservationsByRoomType(string roomType);
        List<HotelReservation> GetReservationsByCheckInDate(DateTime checkInDate);
        List<HotelReservation> GetReservationsByCheckOutDate(DateTime checkOutDate);
        List<HotelReservation> GetReservationsSortedByPriceAscending();
        List<HotelReservation> GetReservationsSortedByPriceDescending();
    }
}