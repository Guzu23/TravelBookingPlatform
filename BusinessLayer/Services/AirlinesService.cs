using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;

namespace BusinessLayer.Services
{
    public class AirlinesService
    {
        private readonly IRepository<Airline> _repository;
        private readonly BusinessLayer.Contracts.ILogger _logger;

        public AirlinesService(IRepository<Airline> _repository, ILogger _logger)
        {
            this._repository = _repository;
            this._logger = _logger;
        }

        public AirlineDto GetAirline(Guid id)
        {
            Airline airline;
            try
            {
                airline = _repository.GetById(id);
                if (airline == null)
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

            var airlineDto = new AirlineDto
            {
                Id = airline.Id,
                Name = airline.Name,
                Location = airline.Location,
                ChairsLeft = airline.ChairsLeft,
            };

            _logger.LogAirlineRequestFromDB(airline);

            return airlineDto;
        }

        public void AddAirline(AirlineDto airlineDto)
        {
            var airline = new Airline
            {
                Name = airlineDto.Name,
                Location = airlineDto.Location,
                ChairsLeft = airlineDto.ChairsLeft,
            };

            _logger.LogAirlineInsertRequestToDB(airline);

            _repository.Add(airline);
            _repository.SaveChanges();
        }

        public void UpdateAirline(Guid id, AirlineDto updatedAirlineDto)
        {
            Airline airline;
            try
            {
                airline = _repository.GetById(id);
                if (airline == null)
                {
                    throw new NullReferenceException();
                }
            }
            catch
            {
                throw new NullReferenceException();
            }

            var oldAirline = airline;

            airline.Name = updatedAirlineDto.Name;
            airline.Location = updatedAirlineDto.Location;
            airline.ChairsLeft = updatedAirlineDto.ChairsLeft;

            _logger.LogAirlineUpdateRequestInDB(oldAirline, airline);

            _repository.Update(airline);
            _repository.SaveChanges();
        }

        public void DeleteAirline(Guid id)
        {
            Airline airline;
            try
            {
                airline = _repository.GetById(id);
                if (airline == null)
                {
                    throw new NullReferenceException();
                }
            }
            catch
            {
                throw new NullReferenceException();
            }

            _logger.LogAirlineDeleteRequestFromDB(airline);

            _repository.Remove(airline);
            _repository.SaveChanges();
        }
    }
}
