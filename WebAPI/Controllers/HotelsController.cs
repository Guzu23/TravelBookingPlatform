#pragma warning disable IDE0058
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly BusinessLayer.Contracts.ILogger _logger;

        public HotelsController(MyDbContext context, BusinessLayer.Contracts.ILogger logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet("GetHotel")]
        public ActionResult<Hotel> GetHotel(Guid id)
        {
            var hotel = _context.Hotels.Find(id);

            if (hotel == null)
            {
                return NotFound();
            }

            var hotelDto = new Hotel
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Location = hotel.Location,
                RoomsLeft = hotel.RoomsLeft,
            };

            _logger.LogHotelRequestFromDB(hotel);

            return Ok(hotelDto);
        }

        [HttpPost("AddHotel")]
        public IActionResult AddHotel(HotelDto hotelDto)
        {
            var hotel = new Hotel
            {
                Name = hotelDto.Name,
                Location = hotelDto.Location,
                RoomsLeft = hotelDto.RoomsLeft,
            };

            _logger.LogHotelInsertRequestToDB(hotel);

            _context.Hotels.Add(hotel);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPatch("UpdateHotel")]
        public IActionResult UpdateHotel(Guid id, HotelDto updatedHotel)
        {
            var hotel = _context.Hotels.FirstOrDefault(r => r.Id == id);
            var oldHotel = hotel;

            if (hotel == null)
            {
                return NotFound();
            }

            hotel.Name = updatedHotel.Name;
            hotel.Location = updatedHotel.Location;
            hotel.RoomsLeft = updatedHotel.RoomsLeft;

            _logger.LogHotelUpdateRequestInDB(oldHotel, hotel);

            _context.Hotels.Update(hotel);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("DeleteHotel")]
        public IActionResult DeleteHotel(Guid id)
        {
            var hotel = _context.Hotels.FirstOrDefault(r => r.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            _logger.LogHotelDeleteRequestFromDB(hotel);

            _context.Hotels.Remove(hotel);
            _context.SaveChanges();

            return Ok();
        }
    }
}
