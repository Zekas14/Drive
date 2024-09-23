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
        [HttpGet("InformTraveller/{tripId:int}")]
        public async Task<IActionResult> InformTraveller(int tripId)
        {
            if (ModelState.IsValid)
            {
                var result = await services.InfromTraveller(tripId);
                if (result.StatusCode == 404)
                {
                    return NotFound(result);  
                }
                    return Ok(new {result});   
            }
                return BadRequest(ModelState);
        }
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
        
        [HttpGet("GetRequestedTrips")]
       //    [Authorize(Roles ="Driver")]
        public IActionResult GetRequstedTrips()
        {
            if (ModelState.IsValid)
            {
                var result = services.GetRequestedTrips();
                if (result.StatusCode == 404)
                {
                return NotFound(result);
                }
                return Ok(result);
            } 
                return BadRequest(ModelState);
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
        [HttpGet("GetTravellerTrip")]
           public IActionResult GetTravellerTrip(string travellerId)
        {
            if (ModelState.IsValid)
            {
                var result = services.GetTravellerTrips(travellerId);
                if (result.StatusCode == 404) 
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);  
        }
    }
}
