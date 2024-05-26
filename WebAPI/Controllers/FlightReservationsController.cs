#pragma warning disable IDE0058
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Dto;
using BusinessLayer.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightReservationsController : ControllerBase
    {
        private readonly FlightReservationsService flightReservationsService;

        public FlightReservationsController(FlightReservationsService flightReservationsService)
        {
            this.flightReservationsService = flightReservationsService;
        }


        [HttpGet("GetFlightReservation")]
        public ActionResult<FlightReservationDto> GetFlightReservation(Guid id)
        {
            var flightReservationDto = this.flightReservationsService.GetFlightReservation(id);

            return Ok(flightReservationDto);
        }

        [HttpPost("AddFlightReservation")]
        public IActionResult AddFlightReservation(FlightReservationDto flightReservationDto)
        {
            this.flightReservationsService.AddFlightReservation(flightReservationDto);

            return Ok();
        }

        [HttpPatch("UpdateFlightReservation")]
        public IActionResult UpdateFlightReservation(Guid id, FlightReservationDto updatedFlightReservationDto)
        {
            flightReservationsService.UpdateFlightReservation(id, updatedFlightReservationDto);

            return Ok();
        }

        [HttpDelete("DeleteFlightReservation")]
        public IActionResult DeleteFlightReservation(Guid id)
        {
            flightReservationsService.DeleteFlightReservation(id);

            return Ok();
        }

    }
}
