using System.ComponentModel.DataAnnotations;

namespace DriveApp.DTO
{
    public class VerifyOtpDto
    {
        [EmailAddress]
        public required string Email { get; set; }   
        [MaxLength(4)]
        public required string InputOtp { get; set; }
    }
}
