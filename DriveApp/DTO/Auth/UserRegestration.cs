using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveApp.DTO

{
    [NotMapped]
    [Index(nameof(Email),nameof(PhoneNumber),IsUnique =true)]
    public class Regestration 
    {
        public required string UserName { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        [Phone]
        public required string PhoneNumber { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [MinLength(3)]
        public required string Address { get; set; }
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [Compare("Password")]
        public required string ConfirmPassword { get; set; }
        [AllowedValues(["Admin", "Traveller", "Driver"])]
        [DefaultValue("Traveller")]
        public required string Role { get; set; }

        public string LicenseNumber { get; set; }
        public string AvailabilityStatus { get; set; }


    }
}
