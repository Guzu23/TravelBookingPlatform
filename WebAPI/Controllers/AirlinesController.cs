#pragma warning disable IDE0058
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirlinesController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly BusinessLayer.Contracts.ILogger _logger;

        public AirlinesController(MyDbContext context, BusinessLayer.Contracts.ILogger logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet("GetAirline")]
        public ActionResult<AirlineDto> GetAirline(Guid id)
        {
            var airline = _context.Airlines.Find(id);

            if (airline == null)
            {
                return NotFound(airline);
            }

            var airlineDto = new AirlineDto
            {
                Id = airline.Id,
                Name = airline.Name,
                Location = airline.Location,
                ChairsLeft = airline.ChairsLeft,
            };

            _logger.LogAirlineRequestFromDB(airline);

            return Ok(airlineDto);
        }

        [HttpPost("AddAirline")]
        public IActionResult AddAirline(AirlineDto airlineDto)
        {
            var airline = new Airline
            {
                Name = airlineDto.Name,
                Location = airlineDto.Location,
                ChairsLeft = airlineDto.ChairsLeft,
            };

            _logger.LogAirlineInsertRequestToDB(airline);

            _context.Airlines.Add(airline);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPatch("UpdateAirline")]
        public IActionResult UpdateAirline(Guid id, AirlineDto updatedAirlineDto)
        {
            var airline = _context.Airlines.FirstOrDefault(r => r.Id == id);
            var oldAirline = airline;

            if (airline == null)
            {
                return NotFound();
            }

            airline.Name = updatedAirlineDto.Name;
            airline.Location = updatedAirlineDto.Location;
            airline.ChairsLeft = updatedAirlineDto.ChairsLeft;

            _logger.LogAirlineUpdateRequestInDB(oldAirline, airline);

            _context.Airlines.Update(airline);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("DeleteAirline")]
        public IActionResult DeleteAirline(Guid id)
        {
            var airline = _context.Airlines.FirstOrDefault(r => r.Id == id);

            if (airline == null)
            {
                return NotFound(airline);
            }

            _logger.LogAirlineDeleteRequestFromDB(airline);

            _context.Airlines.Remove(airline);
            _context.SaveChanges();

            return Ok();
        }

    }
}
