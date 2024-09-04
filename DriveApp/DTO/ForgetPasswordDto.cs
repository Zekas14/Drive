using System.ComponentModel.DataAnnotations;

namespace DriveApp.DTO
{
    public class ForgetPasswordDto
    {
        [EmailAddress]
        public required string Email { get; set; }
    }
}
