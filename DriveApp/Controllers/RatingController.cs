using DriveApp.Core;
using DriveApp.DTO;
using DriveApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController(IRatingServices services) : ControllerBase
    {
        private readonly IRatingServices services = services;
        [HttpPost]
        public IActionResult RateDriver(RateDriverDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = services.RateDriver(dto);
                if (result.StatusCode== 404)
                {
                    return NotFound(result);
                }
                return Created(Request.GetBaseUrl(),result);

            }
            return BadRequest(ModelState);
        }
    }
}
