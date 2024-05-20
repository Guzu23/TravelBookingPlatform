using BusinessLayer.Contracts;
using DataAccessLayer;
using DataAccessLayer.Models;

namespace BusinessLayer.Services.Linq
{
    public class LinqHotelReservation : ILinqHotelReservation
    {
        private readonly MyDbContext _context;

        public LinqHotelReservation(MyDbContext context)
        {
            _context = context;
        }

        public List<HotelReservation> GetReservationsByRoomType(string roomType)
        {
            return _context.HotelReservations
                .Where(r => r.RoomType == roomType)
                .ToList();
        }

        public List<HotelReservation> GetReservationsByCheckInDate(DateTime checkInDate)
        {
            return _context.HotelReservations
                .Where(r => r.CheckIn.Date == checkInDate.Date)
                .ToList();
        }

        public List<HotelReservation> GetReservationsByCheckOutDate(DateTime checkOutDate)
        {
            return _context.HotelReservations
                .Where(r => r.CheckOut.Date == checkOutDate.Date)
                .ToList();
        }

        public List<HotelReservation> GetReservationsSortedByPriceAscending()
        {
            return _context.HotelReservations
                .OrderBy(r => r.Price)
                .ToList();
        }

        public List<HotelReservation> GetReservationsSortedByPriceDescending()
        {
            return _context.HotelReservations
                .OrderByDescending(r => r.Price)
                .ToList();
        }
    }
}