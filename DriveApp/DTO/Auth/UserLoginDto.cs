using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveApp.DTO
{
    [NotMapped]
    public class Login
    {
        [Required]
        public string Email { get; set;}
        [Required]
        public string Password { get; set;}
    }
}
