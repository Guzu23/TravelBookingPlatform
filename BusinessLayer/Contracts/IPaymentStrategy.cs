using BusinessLayer.Dto;

namespace BusinessLayer.Contracts
{
    public interface IPaymentStrategy
    {
        double CalculateTotalPrice(UserDto userDto, List<HotelReservationDto> hotelReservationsDtos, List<FlightReservationDto> flightReservationsDtos);
    }

}
