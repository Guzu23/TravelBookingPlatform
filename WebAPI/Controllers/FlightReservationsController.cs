#pragma warning disable IDE0058
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightReservationsController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly BusinessLayer.Contracts.ILogger _logger;

        public FlightReservationsController(MyDbContext context, BusinessLayer.Contracts.ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("GetFlightReservation")]
        public ActionResult<FlightReservationDto> GetFlightReservation(Guid id)
        {
            var flightReservation = _context.FlightReservations.FirstOrDefault(r => r.Id == id);

            if (flightReservation == null)
            {
                return NotFound();
            }

            var flightReservationDto = new FlightReservationDto
            {
                Id = flightReservation.Id,
                IdAirline = flightReservation.IdAirline,
                IdUser = flightReservation.IdUser,
                Chair = flightReservation.Chair,
                FlightDate = flightReservation.FlightDate,
                FlightDuration = flightReservation.FlightDuration,
                CheckInFlightLocation = flightReservation.CheckInFlightLocation,
                CheckOutFlightLocation = flightReservation.CheckOutFlightLocation,
                Price = flightReservation.Price
            };

            _logger.LogFlightReservationRequestFromDB(flightReservation);

            return Ok(flightReservationDto);
        }

        [HttpPost("AddFlightReservation")]
        public IActionResult AddFlightReservation(FlightReservationDto flightReservationDto)
        {
            var flightReservation = new FlightReservation
            {
                IdAirline = flightReservationDto.IdAirline,
                IdUser = flightReservationDto.IdUser,
                Chair = flightReservationDto.Chair,
                FlightDate = flightReservationDto.FlightDate,
                FlightDuration = flightReservationDto.FlightDuration,
                CheckInFlightLocation = flightReservationDto.CheckInFlightLocation,
                CheckOutFlightLocation = flightReservationDto.CheckOutFlightLocation,
                Price = flightReservationDto.Price 
            };

            _logger.LogFlightReservationInsertRequestToDB(flightReservation);

            _context.FlightReservations.Add(flightReservation);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPatch("UpdateFlightReservation")]
        public IActionResult UpdateFlightReservation(Guid id, FlightReservation updatedFlightReservation)
        {
            var flightReservation = _context.FlightReservations.FirstOrDefault(r => r.Id == id);
            var oldflightReservation = flightReservation;

            if (flightReservation == null)
            {
                return NotFound();
            }

            flightReservation.Chair = updatedFlightReservation.Chair;
            flightReservation.FlightDate = updatedFlightReservation.FlightDate;
            flightReservation.FlightDuration = updatedFlightReservation.FlightDuration;
            flightReservation.CheckInFlightLocation = updatedFlightReservation.CheckInFlightLocation;
            flightReservation.CheckOutFlightLocation = updatedFlightReservation.CheckOutFlightLocation;
            flightReservation.Price = updatedFlightReservation.Price;
            flightReservation.IdAirline = updatedFlightReservation.IdAirline;
            flightReservation.IdUser = updatedFlightReservation.IdUser;

            _logger.LogFlightReservationUpdateRequestInDB(oldflightReservation, flightReservation);

            _context.FlightReservations.Update(flightReservation);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("DeleteFlightReservation")]
        public IActionResult DeleteFlightReservation(Guid id)
        {
            var flightReservation = _context.FlightReservations.FirstOrDefault(r => r.Id == id);

            if (flightReservation == null)
            {
                return NotFound();
            }

            _logger.LogFlightReservationDeleteRequestFromDB(flightReservation);

            _context.FlightReservations.Remove(flightReservation);
            _context.SaveChanges();

            return Ok();
        }
    }
}
