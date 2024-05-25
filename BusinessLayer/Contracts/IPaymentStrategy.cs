using BusinessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IPaymentStrategy
    {
        double CalculateTotalPrice(UserDto userDto, List<HotelReservationDto> hotelReservationsDtos, List<FlightReservationDto> flightReservationsDtos);
    }

}
