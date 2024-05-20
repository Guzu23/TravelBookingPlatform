#pragma warning disable IDE0058
using BusinessLayer.Contracts;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelReservationController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILinqHotelReservation _linqHotelReservation;
        private readonly BusinessLayer.Contracts.ILogger _logger;

        public HotelReservationController(MyDbContext context, ILinqHotelReservation linqHotelReservation, BusinessLayer.Contracts.ILogger logger)
        {
            _context = context;
            _linqHotelReservation = linqHotelReservation;
            _logger = logger;
        }


        [HttpGet("GetHotelReservation")]
        public ActionResult<HotelReservationDto> GetHotelReservation(Guid id)
        {
            var hotelReservation = _context.HotelReservations.FirstOrDefault(r => r.Id == id);

            if (hotelReservation == null)
            {
                return NotFound();
            }

            var hotelReservationDto = new HotelReservationDto
            {
                Id = hotelReservation.Id,
                IdHotel = hotelReservation.IdHotel,
                IdUser = hotelReservation.IdUser,
                RoomType = hotelReservation.RoomType,
                CheckIn = hotelReservation.CheckIn,
                CheckOut = hotelReservation.CheckOut,
                Price = hotelReservation.Price
            };

            _logger.LogHotelReservationRequestFromDB(hotelReservation);

            return Ok(hotelReservationDto);
        }

        [HttpGet("GetHotelReservationsSortedByPriceAscending")]
        public ActionResult<IEnumerable<HotelReservation>> GetHotelReservationsSortedByPriceAscending()
        {
            var sortedReservations = _linqHotelReservation.GetReservationsSortedByPriceAscending();
            return Ok(sortedReservations);
        }

        [HttpGet("GetHotelReservationsSortedByPriceDescending")]
        public ActionResult<IEnumerable<HotelReservation>> GetHotelReservationsSortedByPriceDescending()
        {
            var sortedReservations = _linqHotelReservation.GetReservationsSortedByPriceDescending();
            return Ok(sortedReservations);
        }

        [HttpGet("GetHotelReservationsByRoomType")]
        public ActionResult<List<HotelReservationDto>> GetHotelReservationsByRoomType(string roomType)
        {
            var reservations = _linqHotelReservation.GetReservationsByRoomType(roomType);
            var reservationDtos = reservations.Select(r => new HotelReservationDto
            {
                Id = r.Id,
                IdHotel = r.IdHotel,
                IdUser = r.IdUser,
                RoomType = r.RoomType,
                CheckIn = r.CheckIn,
                CheckOut = r.CheckOut,
                Price = r.Price
            }).ToList();

            return Ok(reservationDtos);
        }

        [HttpGet("GetHotelReservationsByCheckInDate")]
        public ActionResult<List<HotelReservationDto>> GetHotelReservationsByCheckInDate(DateTime datetime)
        {
            var reservations = _linqHotelReservation.GetReservationsByCheckInDate(datetime);
            var reservationDtos = reservations.Select(r => new HotelReservationDto
            {
                Id = r.Id,
                IdHotel = r.IdHotel,
                IdUser = r.IdUser,
                RoomType = r.RoomType,
                CheckIn = r.CheckIn,
                CheckOut = r.CheckOut,
                Price = r.Price
            }).ToList();

            return Ok(reservationDtos);
        }

        [HttpGet("GetHotelReservationsByCheckOutDate")]
        public ActionResult<List<HotelReservationDto>> GetHotelReservationsByCheckOutDate(DateTime datetime)
        {
            var reservations = _linqHotelReservation.GetReservationsByCheckOutDate(datetime);
            var reservationDtos = reservations.Select(r => new HotelReservationDto
            {
                Id = r.Id,
                IdHotel = r.IdHotel,
                IdUser = r.IdUser,
                RoomType = r.RoomType,
                CheckIn = r.CheckIn,
                CheckOut = r.CheckOut,
                Price = r.Price
            }).ToList();

            return Ok(reservationDtos);
        }

        [HttpPost("AddHotelReservation")]
        public IActionResult AddHotelReservation(HotelReservationDto hotelReservationDto)
        {
            var hotelReservation = new HotelReservation
            {
                IdHotel = hotelReservationDto.IdHotel,
                IdUser = hotelReservationDto.IdUser,
                CheckIn = hotelReservationDto.CheckIn,
                CheckOut = hotelReservationDto.CheckOut,
                RoomType = hotelReservationDto.RoomType,
                Price = hotelReservationDto.Price
            };

            _logger.LogHotelReservationInsertRequestToDB(hotelReservation);

            _context.HotelReservations.Add(hotelReservation);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPatch("UpdateHotelReservation")]
        public IActionResult UpdateHotelReservation(Guid id, HotelReservationDto updatedHotelReservation)
        {
            var hotelReservation = _context.HotelReservations.FirstOrDefault(r => r.Id == id);
            var oldHotelReservation = hotelReservation;
            if (hotelReservation == null)
            {
                return NotFound();
            }

            hotelReservation.CheckIn = updatedHotelReservation.CheckIn;
            hotelReservation.CheckOut = updatedHotelReservation.CheckOut;
            hotelReservation.RoomType = updatedHotelReservation.RoomType;
            hotelReservation.Price = updatedHotelReservation.Price;
            hotelReservation.IdHotel = updatedHotelReservation.IdHotel;
            hotelReservation.IdUser = updatedHotelReservation.IdUser;

            _logger.LogHotelReservationUpdateRequestInDB(oldHotelReservation, hotelReservation);

            _context.HotelReservations.Update(hotelReservation);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("DeleteHotelReservation")]
        public IActionResult DeleteHotelReservation(Guid id)
        {
            var hotelReservation = _context.HotelReservations.FirstOrDefault(r => r.Id == id);

            if (hotelReservation == null)
            {
                return NotFound();
            }

            _logger.LogHotelReservationDeleteRequestFromDB(hotelReservation);

            _context.HotelReservations.Remove(hotelReservation);
            _context.SaveChanges();

            return Ok();
        }

    }
}