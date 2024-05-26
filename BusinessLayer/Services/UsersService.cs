using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
namespace BusinessLayer.Services
{
    public class UsersService
    {
        private readonly IRepository<User> _repository;
        private readonly IRepository<HotelReservation> _hotelReservationRepository;
        private readonly IRepository<FlightReservation> _flightReservationRepository;
        private readonly BusinessLayer.Contracts.ILogger _logger;
        private readonly IUserDelegate _userDelegate;


        public UsersService(IRepository<User> _repository, ILogger _logger, IUserDelegate userDelegate, IRepository<HotelReservation> _hotelReservationRepository, IRepository<FlightReservation> _flightReservationRepository)
        {
            this._repository = _repository;
            this._hotelReservationRepository = _hotelReservationRepository;
            this._flightReservationRepository = _flightReservationRepository;
            this._logger = _logger;
            this._userDelegate = userDelegate;
            #pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            this._userDelegate.UserActivated += OnUserActivated;
            this._userDelegate.UserDeactivated += OnUserDeactivated;
            #pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).

        }

        public UserDto? GetUser(Guid id)
        {
            User user;
            try
            {
                user = _repository.GetById(id);
                if (user == null)
                {
                    _logger.LogWarningUserNotExisting(id);
                    return null;
                }
            }
            catch
            {
                _logger.LogErrorSomethingWentWrongWithUser(id);
                return null;
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

            return userDto;
        }

        public List<HotelReservationDto>? GetUserHotelReservations(Guid id)
        {
            User user;
            try
            {
                user = _repository.GetById(id);
                if (user == null)
                {
                    _logger.LogWarningUserNotExisting(id);
                    return null;
                }
            }
            catch
            {
                _logger.LogErrorSomethingWentWrongWithUser(id);
                return null;
            }

            List<HotelReservation> hotelReservations; 
            try
            {
                hotelReservations = _hotelReservationRepository
                                            .Find(hr => hr.IdUser == id)
                                            .ToList();
                if(hotelReservations == null)
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

            var hotelReservationsDto = hotelReservations.Select(hr => new HotelReservationDto
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

            foreach (var hr in hotelReservations)
            {
                _logger.LogHotelReservationRequestFromDB(hr);
            }

            return hotelReservationsDto;
        }

        public List<FlightReservationDto>? GetUserFlightReservations(Guid id)
        {
            User user;
            try
            {
                user = _repository.GetById(id);
                if (user == null)
                {
                    _logger.LogWarningUserNotExisting(id);
                    return null;
                }
            }
            catch
            {
                _logger.LogErrorSomethingWentWrongWithUser(id);
                return null;
            }

            List<FlightReservation> flightReservations;
            try
            {
                flightReservations = _flightReservationRepository
                                            .Find(hr => hr.IdUser == id)
                                            .ToList();
                if (flightReservations == null)
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

            var flightReservationsDto = flightReservations.Select(fr => new FlightReservationDto
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

            return flightReservationsDto;
        }

        public void AddUser(UserDto userDto)
        {
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

            _repository.Add(user);
            _repository.SaveChanges();

            _userDelegate.OnUserActivated(user);
        }

        public void UpdateUser(Guid id, UserDto updatedUserDto)
        {
            User user;
            try
            {
                user = _repository.GetById(id);
                if (user == null)
                {
                    _logger.LogWarningUserNotExisting(id);
                    return;
                }
            }
            catch
            {
                _logger.LogErrorSomethingWentWrongWithUser(id);
                return;
            }

            var oldUser = user;

            user.Username = updatedUserDto.Username;
            user.Email = updatedUserDto.Email;

            if (!string.IsNullOrWhiteSpace(updatedUserDto.Password))
            {
                user.Password = PasswordHelper.HashPassword(updatedUserDto.Password);
            }

            user.Active = updatedUserDto.Active;
            user.ImageProfile = updatedUserDto.ImageProfile;

            _logger.LogUserUpdateRequestInDB(oldUser, user);

            _repository.Update(user);
            _repository.SaveChanges();

            if (user.Active)
            {
                _userDelegate.OnUserActivated(user);
            }
            else
            {
                _userDelegate.OnUserDeactivated(user);
            }
        }

        public void ActivateUser(Guid id) 
        {
            User user;
            try
            {
                user = _repository.GetById(id);
                if (user == null)
                {
                    _logger.LogWarningUserNotExisting(id);
                    return;
                }
            }
            catch
            {
                _logger.LogErrorSomethingWentWrongWithUser(id);
                return;
            }

            var oldUser = user;

            user.Active = true;

            _userDelegate.OnUserActivated(user);
            _logger.LogUserUpdateRequestInDB(oldUser, user);

            _repository.Update(user);
            _repository.SaveChanges();
        }

        public void DeactivateUser(Guid id)
        {
            User user;
            try
            {
                user = _repository.GetById(id);
                if (user == null)
                {
                    _logger.LogWarningUserNotExisting(id);
                    return;
                }
            }
            catch
            {
                _logger.LogErrorSomethingWentWrongWithUser(id);
                return;
            }

            var oldUser = user;

            user.Active = false;

            _userDelegate.OnUserActivated(user);
            _logger.LogUserUpdateRequestInDB(oldUser, user);

            _repository.Update(user);
            _repository.SaveChanges();
        }

        public void DeleteUser(Guid id)
        {
            User user;
            try
            {
                user = _repository.GetById(id);
                if (user == null)
                {
                    _logger.LogWarningUserNotExisting(id);
                    return;
                }
            }
            catch
            {
                _logger.LogErrorSomethingWentWrongWithUser(id);
                return;
            }

            _logger.LogUserDeleteRequestFromDB(user);

            _repository.Remove(user);
            _repository.SaveChanges();
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

        //Strategy pattern
        public double GetUserHasToPay(Guid id)
        {
            User user;
            try
            {
                user = _repository.GetById(id);
                if (user == null)
                {
                    _logger.LogWarningUserNotExisting(id);
                    return -1;
                }
            }
            catch
            {
                _logger.LogErrorSomethingWentWrongWithUser(id);
                return -1;
            }

            PaymentService paymentService = new PaymentService(this);
            return paymentService.CalculatePayment(id);
        }
    }
}
