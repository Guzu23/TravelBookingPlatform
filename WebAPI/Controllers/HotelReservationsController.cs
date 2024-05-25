#pragma warning disable IDE0058
using BusinessLayer.Contracts;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Dto;
using BusinessLayer.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelReservationController : ControllerBase
    {
        private readonly HotelReservationsService hotelReservationsService;
        private readonly ILinqHotelReservation _linqHotelReservation;

        public HotelReservationController(HotelReservationsService hotelReservationsService, ILinqHotelReservation linqHotelReservation)
        {
            this.hotelReservationsService = hotelReservationsService;
            this._linqHotelReservation = linqHotelReservation;
        }


        [HttpGet("GetHotelReservation")]
        public ActionResult<HotelReservationDto> GetHotelReservation(Guid id)
        {
            var hotelReservationDto = hotelReservationsService.GetHotelReservation(id);
            return Ok(hotelReservationDto);
        }

        [HttpGet("GetHotelReservationsSortedByPriceAscending")]
        public ActionResult<IEnumerable<HotelReservationDto>> GetHotelReservationsSortedByPriceAscending()
        {
            var sortedReservations = _linqHotelReservation.GetReservationsSortedByPriceAscending();
            return Ok(sortedReservations);
        }

        [HttpGet("GetHotelReservationsSortedByPriceDescending")]
        public ActionResult<IEnumerable<HotelReservationDto>> GetHotelReservationsSortedByPriceDescending()
        {
            var sortedReservations = _linqHotelReservation.GetReservationsSortedByPriceDescending();
            return Ok(sortedReservations);
        }

        [HttpGet("GetHotelReservationsByRoomType")]
        public ActionResult<List<HotelReservationDto>> GetHotelReservationsByRoomType(string roomType)
        {
            var reservationDtos = _linqHotelReservation.GetReservationsByRoomType(roomType);
            return Ok(reservationDtos);
        }

        [HttpGet("GetHotelReservationsByCheckInDate")]
        public ActionResult<List<HotelReservationDto>> GetHotelReservationsByCheckInDate(DateTime datetime)
        {
            var reservationDtos = _linqHotelReservation.GetReservationsByCheckInDate(datetime);
            return Ok(reservationDtos);
        }

        [HttpGet("GetHotelReservationsByCheckOutDate")]
        public ActionResult<List<HotelReservationDto>> GetHotelReservationsByCheckOutDate(DateTime datetime)
        {
            var reservationDtos = _linqHotelReservation.GetReservationsByCheckOutDate(datetime);
            return Ok(reservationDtos);
        }

        [HttpPost("AddHotelReservation")]
        public IActionResult AddHotelReservation(HotelReservationDto hotelReservationDto)
        {
            this.hotelReservationsService.AddHotelReservation(hotelReservationDto);
            return Ok();
        }

        [HttpPatch("UpdateHotelReservation")]
        public IActionResult UpdateHotelReservation(Guid id, HotelReservationDto updatedHotelReservation)
        {
            this.hotelReservationsService.UpdateHotelReservation(id, updatedHotelReservation);
            return Ok();
        }

        [HttpDelete("DeleteHotelReservation")]
        public IActionResult DeleteHotelReservation(Guid id)
        {
            this.hotelReservationsService.DeleteHotelReservation(id);
            return Ok();
        }

    }
}