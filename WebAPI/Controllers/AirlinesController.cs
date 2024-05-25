#pragma warning disable IDE0058
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Dto;
using BusinessLayer.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirlinesController : ControllerBase
    {
        private readonly AirlinesService airlinesService;

        public AirlinesController(AirlinesService airlinesService)
        {
            this.airlinesService = airlinesService;
        }


        [HttpGet("GetAirline")]
        public ActionResult<AirlineDto> GetAirline(Guid id)
        {
            var airlineDto = this.airlinesService.GetAirline(id);
            if (airlineDto == null) return NotFound();
            return Ok(airlineDto);
        }

        [HttpPost("AddAirline")]
        public IActionResult AddAirline(AirlineDto airlineDto)
        {
            this.airlinesService.AddAirline(airlineDto);

            return Ok();
        }

        [HttpPatch("UpdateAirline")]
        public IActionResult UpdateAirline(Guid id, AirlineDto updatedAirlineDto)
        {
            airlinesService.UpdateAirline(id, updatedAirlineDto);

            return Ok();
        }

        [HttpDelete("DeleteAirline")]
        public IActionResult DeleteAirline(Guid id)
        {
            airlinesService.DeleteAirline(id);

            return Ok();
        }

    }
}
