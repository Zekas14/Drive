using DriveApp.DTO;
using DriveApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Driver")]
    public class DriverController(ITripServices services) : ControllerBase
    {
        private readonly ITripServices services = services;
        [HttpPost("AcceptTrip")]
        public async Task<IActionResult> AcceptTrip(AcceptTripDto dto)
        {
            var result = await services.AcceptTrip(dto);
            if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            return Accepted(result);
        }
    }
}
