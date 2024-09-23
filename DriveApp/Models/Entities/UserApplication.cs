using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DriveApp.Models.Entities
{
    [Index(nameof(Email),nameof(PhoneNumber),IsUnique =true)]    
    
    public class UserApplication : IdentityUser
    {
        [DataType(DataType.Text)]
        public string Address { get; set; }
    }
}
