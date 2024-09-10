using System.ComponentModel.DataAnnotations;
using DriveApp.Models.Data;

namespace DriveApp.DTO
{
    public class TripDetailsDto : IValidatableObject
    {
        public int TripId { get; set; }
        public DateTime DatebyHour { get; set; }= DateTime.Now;
        public string DriverId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var conetxt = (AppDbContext)validationContext.GetService(typeof(AppDbContext))!;
            var user = conetxt.Users.FirstOrDefault(u => u.Id == this.DriverId);
            var trip = conetxt.Trips.FirstOrDefault(u => u.Id == this.TripId);
            if (user is null)
            {
                yield return new ValidationResult("Driver not Found");
            }
            if (trip is null)
            {
                yield return new ValidationResult("Trip not Found");
            }
        }
    }
}
