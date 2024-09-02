using DriveApp.Core.Enums;
using DriveApp.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveApp.DTO

{
    [NotMapped]
    public class Regestration : IValidatableObject
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
        [AllowedValues(["Admin","Traveller","Driver"])]
        [DefaultValue("Traveller")]
        public required string Role { get; set; }  
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dbContext = (AppDbContext)validationContext.GetService(typeof(AppDbContext))!;

            if (dbContext.Users.Any(u => u.Email == this.Email))
            {
                yield return new ValidationResult("Email must be unique.");
            }
            if(dbContext.Users.Any(u => u.PhoneNumber == this.PhoneNumber))
            {
                yield return new ValidationResult("Phone Number must be unique.");

            }
        }


    }
}
