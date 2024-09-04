using System.ComponentModel.DataAnnotations;

namespace DriveApp.DTO.Auth
{
    public class ForgetPasswordDto
    {
        [EmailAddress]
        public required string Email { get; set; }
    }
}
