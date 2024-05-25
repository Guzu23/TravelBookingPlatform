using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class HotelsService
    {
        private readonly IRepository<Hotel> _repository;
        private readonly BusinessLayer.Contracts.ILogger _logger;

        public HotelsService(IRepository<Hotel> _repository, ILogger _logger)
        {
            this._repository = _repository;
            this._logger = _logger;
        }

        public HotelDto GetHotel(Guid id)
        {
            Hotel hotel;
            try
            {
                hotel = _repository.GetById(id);
                if (hotel == null)
                {
                    throw new NullReferenceException();
                }
            }
            catch
            {
                throw new NullReferenceException();
            }

            var hotelDto = new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Location = hotel.Location,
                RoomsLeft = hotel.RoomsLeft,
            };

            _logger.LogHotelRequestFromDB(hotel);

            return hotelDto;
        }

        public void AddHotel(HotelDto hotelDto)
        {
            var hotel = new Hotel
            {
                Name = hotelDto.Name,
                Location = hotelDto.Location,
                RoomsLeft = hotelDto.RoomsLeft,
            };

            _logger.LogHotelInsertRequestToDB(hotel);

            _repository.Add(hotel);
            _repository.SaveChanges();
        }

        public void UpdateHotel(Guid id, HotelDto updatedHotelDto)
        {
            Hotel hotel;
            try
            {
                hotel = _repository.GetById(id);
                if (hotel == null)
                {
                    throw new NullReferenceException();
                }
            }
            catch
            {
                throw new NullReferenceException();
            }

            var oldHotel = hotel;

            hotel.Name = updatedHotelDto.Name;
            hotel.Location = updatedHotelDto.Location;
            hotel.RoomsLeft = updatedHotelDto.RoomsLeft;

            _logger.LogHotelUpdateRequestInDB(oldHotel, hotel);

            _repository.Update(hotel);
            _repository.SaveChanges();
        }

        public void DeleteHotel(Guid id)
        {
            Hotel hotel;
            try
            {
                hotel = _repository.GetById(id);
                if (hotel == null)
                {
                    throw new NullReferenceException();
                }
            }
            catch
            {
                throw new NullReferenceException();
            }

            _logger.LogHotelDeleteRequestFromDB(hotel);

            _repository.Remove(hotel);
            _repository.SaveChanges();
        }
    }
}
