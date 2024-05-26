#pragma warning disable IDE0058
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Dto;
using BusinessLayer.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly HotelsService hotelsService;

        public HotelsController(HotelsService hotelsService)
        {
            this.hotelsService = hotelsService;
        }


        [HttpGet("GetHotel")]
        public ActionResult<Hotel> GetHotel(Guid id)
        {
            var hotelDto = this.hotelsService.GetHotel(id);
            return Ok(hotelDto);
        }

        [HttpPost("AddHotel")]
        public IActionResult AddHotel(HotelDto hotelDto)
        {
            this.hotelsService.AddHotel(hotelDto);
            return Ok();
        }

        [HttpPatch("UpdateHotel")]
        public IActionResult UpdateHotel(Guid id, HotelDto updatedHotel)
        {
            this.hotelsService.UpdateHotel(id, updatedHotel);
            return Ok();
        }

        [HttpDelete("DeleteHotel")]
        public IActionResult DeleteHotel(Guid id)
        {
            this.hotelsService.DeleteHotel(id);
            return Ok();
        }
    }
}
