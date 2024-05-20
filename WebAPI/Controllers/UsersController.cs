#pragma warning disable IDE0058
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using BusinessLayer.Services;
using BusinessLayer.Contracts;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IUserDelegate _userDelegate;
        private readonly BusinessLayer.Contracts.ILogger _logger;
        public UsersController(MyDbContext context, IUserDelegate userDelegate, BusinessLayer.Contracts.ILogger logger)
        {
            _context = context;
            _userDelegate = userDelegate;
            _userDelegate.UserActivated += OnUserActivated;
            _userDelegate.UserDeactivated += OnUserDeactivated;
            _logger = logger;
        }

        [HttpGet("GetUser")]
        public ActionResult<UserDto> GetUser(Guid id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Active = user.Active,
                ImageProfile = user.ImageProfile
            };

            _logger.LogUserRequestFromDB(user);

            return Ok(userDto);
        }

        [HttpGet("GetUserHotelReservations")]
        public ActionResult<List<HotelReservationDto>> GetUserHotelReservations(Guid id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            var hotelReservations = _context.HotelReservations
                                            .Where(hr => hr.IdUser == id)
                                            .ToList();

            var hotelReservationDtos = hotelReservations.Select(hr => new HotelReservationDto
            {
                Id = hr.Id,
                IdHotel = hr.IdHotel,
                IdUser = hr.IdUser,
                RoomType = hr.RoomType,
                CheckIn = hr.CheckIn,
                CheckOut = hr.CheckOut,
                Price = hr.Price
            }).ToList();

            _logger.LogUserRequestFromDB(user);

            foreach(var hr in hotelReservations)
            {
                _logger.LogHotelReservationRequestFromDB(hr);
            }

            return Ok(hotelReservationDtos);
        }

        [HttpGet("GetUserFlightReservations")]
        public ActionResult<List<FlightReservationDto>> GetUserFlightReservations(Guid id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            var flightReservations = _context.FlightReservations
                                            .Where(fr => fr.IdUser == id)
                                            .ToList();

            var flightReservationDtos = flightReservations.Select(fr => new FlightReservationDto
            {
                Id = fr.Id,
                IdAirline = fr.IdAirline,
                IdUser = fr.IdUser,
                Chair = fr.Chair,
                FlightDate = fr.FlightDate,
                FlightDuration = fr.FlightDuration,
                CheckInFlightLocation = fr.CheckInFlightLocation,
                CheckOutFlightLocation = fr.CheckOutFlightLocation,
                Price = fr.Price
            }).ToList();

            _logger.LogUserRequestFromDB(user);

            foreach (var fr in flightReservations)
            {
                _logger.LogFlightReservationRequestFromDB(fr);
            }

            return Ok(flightReservationDtos);
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hashedPassword = PasswordHelper.HashPassword(userDto.Password);

            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Password = hashedPassword,
                Active = userDto.Active,
                ImageProfile = userDto.ImageProfile
            };

            _logger.LogUserInsertRequestToDB(user);

            _context.Users.Add(user);
            _context.SaveChanges();

            _userDelegate.OnUserActivated(user);

            return Ok();
        }

        [HttpPatch("UpdateUser")]
        public IActionResult UpdateUser(Guid id, UserDto updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            var oldUser = user;

            if (user == null)
            {
                return NotFound();
            }

            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;

            if (!string.IsNullOrWhiteSpace(updatedUser.Password))
            {
                user.Password = PasswordHelper.HashPassword(updatedUser.Password);
            }

            user.Active = updatedUser.Active;
            user.ImageProfile = updatedUser.ImageProfile;

            _logger.LogUserUpdateRequestInDB(oldUser, user);

            _context.Users.Update(user);
            _context.SaveChanges();

            if (user.Active)
            {
                _userDelegate.OnUserActivated(user);
            }
            else
            {
                _userDelegate.OnUserDeactivated(user);
            }

            return Ok();
        }

        [HttpPut("ActivateUser")]
        public IActionResult ActivateUser(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            var oldUser = user;

            if (user == null)
            {
                return NotFound();
            }

            user.Active = true;

            _userDelegate.OnUserActivated(user);
            _logger.LogUserUpdateRequestInDB(oldUser, user);

            _context.Users.Update(user);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("DeactivateUser")]
        public IActionResult DeactivateUser(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            var oldUser = user;

            if (user == null)
            {
                return NotFound();
            }

            user.Active = false;

            _userDelegate.OnUserDeactivated(user);
            _logger.LogUserUpdateRequestInDB(oldUser, user);

            _context.Users.Update(user);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(Guid id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok();
        }

        private void OnUserActivated(object sender, UserEventArgs e)
        {
            // extract user
            var user = e.User;
            Console.WriteLine($"User activated: {user.Username}");
        }

        private void OnUserDeactivated(object sender, UserEventArgs e)
        {
            var user = e.User;
            Console.WriteLine($"User deactivated: {user.Username}");
        }

    }
}
