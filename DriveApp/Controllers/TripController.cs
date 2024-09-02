using DriveApp.Core;
using DriveApp.DTO;
using DriveApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DriveApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController(ITripServices services) : ControllerBase
    {
        private readonly ITripServices services = services;
        [HttpPatch("CancelTrip/{id:int}")]
        public async Task<IActionResult> CancelTrip(int id)
        {
            var result = await services.CancelTrip(id);
            if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            return Accepted(result);
        }
        [HttpPatch("AcceptTrip/{id:int}")]
        
        public async Task<IActionResult> AcceptTrip(int id)
        {
            var result = await services.AcceptTrip(id);
            if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            return Accepted(result);
        }
        [HttpGet("GetRequestedTrips")]
       //    [Authorize(Roles ="Driver")]
        public IActionResult GetRequstedTrips()
        {
            try
            {
                var trips = services.GetTrips();
               
                return Ok(trips);
            }
            catch
            {
                
                return BadRequest();
            }
        }
        
        [HttpPost("RequestTrip")]
        [Authorize(Roles ="Traveller")]
        public IActionResult RequestTrip(TripDto tripDto)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine($"User :{tripDto.UserId}");
                var data = services.RequestTrip(tripDto);
                var baseUrl = Request.GetBaseUrl();
                var locationUrl = $"{baseUrl}/api/Trip/RequestTrip";
                return Created(locationUrl, data);
            }
            return BadRequest(ModelState);
        }

    }
}
