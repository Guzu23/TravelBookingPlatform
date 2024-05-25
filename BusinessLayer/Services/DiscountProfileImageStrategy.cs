using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class DiscountProfileImageStrategy : IPaymentStrategy
    {
        public double CalculateTotalPrice(UserDto userDto, List<HotelReservationDto> hotelReservationsDtos, List<FlightReservationDto> flightReservationsDtos)
        {
            double totalPrice = 0;

            foreach (var hotelReservationDto in hotelReservationsDtos)
            {
                totalPrice += hotelReservationDto.Price;
            }

            //Users with an image profile(not the default one) get a discount of 5% to flight reservations for security reasons.
            foreach (var flightReservationDto in flightReservationsDtos)
            {
                totalPrice += flightReservationDto.Price * 0.95;
            }

            //Then the same users get another 3% discount to the total price.
            totalPrice *= 0.97;

            return totalPrice;
        }
    }

}
