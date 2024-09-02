using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DriveApp.DTO
{
    public class ResetPasswordDto
    {
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        public string NewPassword { get; set; }
        public string InputOtp { get; set; } 
    }
}
