using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;

namespace BusinessLayer.Services
{
    public class LinqHotelReservation : ILinqHotelReservation
    {
        private readonly IRepository<HotelReservation> _repository;

        public LinqHotelReservation(IRepository<HotelReservation> _repository)
        {
            this._repository = _repository;
        }

        public List<HotelReservationDto> GetReservationsByRoomType(string roomType)
        {
            var hotelReservations = _repository
                                        .Find(r => r.RoomType == roomType)
                                        .ToList();

            var hotelReservationsDto = hotelReservations.Select(r => new HotelReservationDto
            {
                Id = r.Id,
                IdHotel = r.IdHotel,
                IdUser = r.IdUser,
                RoomType = r.RoomType,
                CheckIn = r.CheckIn,
                CheckOut = r.CheckOut,
                Price = r.Price
            }).ToList();

            return hotelReservationsDto;
        }

        public List<HotelReservationDto> GetReservationsByCheckInDate(DateTime checkInDate)
        {
            var hotelReservations = _repository.Find(r => r.CheckIn.Date == checkInDate.Date)
                .ToList();

            var hotelReservationsDto = hotelReservations.Select(r => new HotelReservationDto
            {
                Id = r.Id,
                IdHotel = r.IdHotel,
                IdUser = r.IdUser,
                RoomType = r.RoomType,
                CheckIn = r.CheckIn,
                CheckOut = r.CheckOut,
                Price = r.Price
            }).ToList();

            return hotelReservationsDto;
        }

        public List<HotelReservationDto> GetReservationsByCheckOutDate(DateTime checkOutDate)
        {
            var hotelReservations = _repository.Find(r => r.CheckOut.Date == checkOutDate.Date)
                .ToList();

            var hotelReservationsDto = hotelReservations.Select(r => new HotelReservationDto
            {
                Id = r.Id,
                IdHotel = r.IdHotel,
                IdUser = r.IdUser,
                RoomType = r.RoomType,
                CheckIn = r.CheckIn,
                CheckOut = r.CheckOut,
                Price = r.Price
            }).ToList();

            return hotelReservationsDto;
        }

        public List<HotelReservationDto> GetReservationsSortedByPriceAscending()
        {
            var hotelReservations = _repository.GetAll()
                .OrderBy(r => r.Price)
                .ToList();

            var hotelReservationsDto = hotelReservations.Select(r => new HotelReservationDto
            {
                Id = r.Id,
                IdHotel = r.IdHotel,
                IdUser = r.IdUser,
                RoomType = r.RoomType,
                CheckIn = r.CheckIn,
                CheckOut = r.CheckOut,
                Price = r.Price
            }).ToList();

            return hotelReservationsDto;
        }

        public List<HotelReservationDto> GetReservationsSortedByPriceDescending()
        {
            var hotelReservations = _repository.GetAll()
                .OrderByDescending(r => r.Price)
                .ToList();

            var hotelReservationsDto = hotelReservations.Select(r => new HotelReservationDto
            {
                Id = r.Id,
                IdHotel = r.IdHotel,
                IdUser = r.IdUser,
                RoomType = r.RoomType,
                CheckIn = r.CheckIn,
                CheckOut = r.CheckOut,
                Price = r.Price
            }).ToList();

            return hotelReservationsDto;
        }
    }
}