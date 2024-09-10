using Microsoft.AspNetCore.Identity;
using Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveApp.Models.Entities
{    
    public class UserApplication : IdentityUser
    {
        [DataType(DataType.Text)]
        public string Address { get; set; }
        public ICollection<Trip> Trips { get; set; }= new List<Trip>(); 
    }
}
