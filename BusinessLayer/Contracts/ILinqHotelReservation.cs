using BusinessLayer.Dto;

namespace BusinessLayer.Contracts
{
    public interface ILinqHotelReservation
    {
        List<HotelReservationDto> GetReservationsByRoomType(string roomType);
        List<HotelReservationDto> GetReservationsByCheckInDate(DateTime checkInDate);
        List<HotelReservationDto> GetReservationsByCheckOutDate(DateTime checkOutDate);
        List<HotelReservationDto> GetReservationsSortedByPriceAscending();
        List<HotelReservationDto> GetReservationsSortedByPriceDescending();
    }
}