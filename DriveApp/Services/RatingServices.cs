using DriveApp.Core.Errors;
using DriveApp.DTO;
using DriveApp.Models.Data;
using DriveApp.Models.Entities;
using DriveApp.Services.Interfaces;

namespace DriveApp.Services
{
    public class RatingServices(AppDbContext context) : IRatingServices
    {
        private readonly AppDbContext _context =  context;
        public ApiResponse RateDriver(RateDriverDto dto)
        {
            try
            {
               var driver =  _context.Drivers.Find(dto.DriverId);
                if (driver is null)
                {
                    return new(404, "Driver Not Found");
                }

               Rating rating = new()
               {
                   DriverId = dto.DriverId,
                   Comment = dto.Comment,
                   RateNumber = dto.Rate,       
               };
               _context.Ratings.Add(rating);
               _context.SaveChanges();
                return new(200,"Rating Successful");
            }catch(Exception e)
            {
                return new(500, e.Message);
            }
        }
    }
}
