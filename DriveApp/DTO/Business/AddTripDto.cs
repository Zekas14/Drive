using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;
using DriveApp.Models.Data;

namespace DriveApp.DTO
{
    public class TripDto :IValidatableObject
    {
        
        public required string UserLocation { get; set; }
        public required string Destination { get; set; }
        [AllowedValues(["Pending", "Accepted", "Ended", "Cancelled"])]
        [DefaultValue("Pending")]
        [MaxLength(25)]
        public string? Status { get; set; } = "Pending";
        public decimal Price { get; set; }
        public required string UserId { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var conetxt = (AppDbContext)validationContext.GetService(typeof(AppDbContext))!;
            var user = conetxt.Users.FirstOrDefault(u => u.Id == this.UserId);
            if (user is null)
            {
                yield return new ValidationResult("User not Found");
            }
        }
    }
}
