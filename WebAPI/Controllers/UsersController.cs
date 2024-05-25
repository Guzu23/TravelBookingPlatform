#pragma warning disable IDE0058
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Dto;
using BusinessLayer.Services;
using BusinessLayer.Contracts;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService usersService;

        public UsersController(UsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet("GetUser")]
        public ActionResult<UserDto> GetUser(Guid id)
        {
            var userDto = usersService.GetUser(id);
            return Ok(userDto);
        }

        [HttpGet("GetUserHotelReservations")]
        public ActionResult<List<HotelReservationDto>> GetUserHotelReservations(Guid id)
        {
            var hotelReservationsDto = this.usersService.GetUserHotelReservations(id);
            return Ok(hotelReservationsDto);
        }

        [HttpGet("GetUserFlightReservations")]
        public ActionResult<List<FlightReservationDto>> GetUserFlightReservations(Guid id)
        {
            var flightReservationsDto = this.usersService.GetUserFlightReservations(id);
            return Ok(flightReservationsDto);
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(UserDto userDto)
        {
            this.usersService.AddUser(userDto);
            return Ok();
        }

        [HttpPatch("UpdateUser")]
        public IActionResult UpdateUser(Guid id, UserDto updatedUser)
        {
            this.usersService.UpdateUser(id, updatedUser);
            return Ok();
        }

        [HttpPut("ActivateUser")]
        public IActionResult ActivateUser(Guid id)
        {
            this.usersService.ActivateUser(id);
            return Ok();
        }

        [HttpPut("DeactivateUser")]
        public IActionResult DeactivateUser(Guid id)
        {
            this.usersService.DeactivateUser(id);
            return Ok();
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(Guid id)
        {
            this.usersService.DeleteUser(id);
            return Ok();
        }
    }
}
