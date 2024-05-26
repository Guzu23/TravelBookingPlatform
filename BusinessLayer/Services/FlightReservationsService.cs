using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;

namespace BusinessLayer.Services
{
    public class FlightReservationsService
    {
        private readonly IRepository<FlightReservation> _repository;
        private readonly BusinessLayer.Contracts.ILogger _logger;

        public FlightReservationsService(IRepository<FlightReservation> _repository, ILogger _logger)
        {
            this._repository = _repository;
            this._logger = _logger;
        }

        public FlightReservationDto GetFlightReservation(Guid id)
        {
            FlightReservation flightReservation;
            try
            {
                flightReservation = _repository.GetById(id);
                if (flightReservation == null)
                {
                    throw new NullReferenceException();
                }
            }
            catch
            {
                throw new NullReferenceException();
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

            return flightReservationDto;
        }

        public void AddFlightReservation(FlightReservationDto flightReservationDto)
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

            _repository.Add(flightReservation);
            _repository.SaveChanges();
        }

        public void UpdateFlightReservation(Guid id, FlightReservationDto updatedFlightReservationDto)
        {
            FlightReservation flightReservation;
            try
            {
                flightReservation = _repository.GetById(id);
                if (flightReservation == null)
                {
                    throw new NullReferenceException();
                }
            }
            catch
            {
                throw new NullReferenceException();
            }

            var oldFlightReservation = flightReservation;

            flightReservation.Chair = updatedFlightReservationDto.Chair;
            flightReservation.FlightDate = updatedFlightReservationDto.FlightDate;
            flightReservation.FlightDuration = updatedFlightReservationDto.FlightDuration;
            flightReservation.CheckInFlightLocation = updatedFlightReservationDto.CheckInFlightLocation;
            flightReservation.CheckOutFlightLocation = updatedFlightReservationDto.CheckOutFlightLocation;
            flightReservation.Price = updatedFlightReservationDto.Price;
            flightReservation.IdAirline = updatedFlightReservationDto.IdAirline;
            flightReservation.IdUser = updatedFlightReservationDto.IdUser;

            _logger.LogFlightReservationUpdateRequestInDB(oldFlightReservation, flightReservation);

            _repository.Update(flightReservation);
            _repository.SaveChanges();
        }

        public void DeleteFlightReservation(Guid id)
        {
            FlightReservation flightReservation;
            try
            {
                flightReservation = _repository.GetById(id);
                if (flightReservation == null)
                {
                    throw new NullReferenceException();
                }
            }
            catch
            {
                throw new NullReferenceException();
            }

            _logger.LogFlightReservationDeleteRequestFromDB(flightReservation);

            _repository.Remove(flightReservation);
            _repository.SaveChanges();
        }
    }
}
