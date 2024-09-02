using DriveApp.Models.Data;
using DriveApp.Models.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Trip 
    {
        public int Id { get; set; }
        public required string To { get; set; }
        public required string From { get; set; }
        [AllowedValues(["Pending","Accepted","Ended"])]
        [DefaultValue("Pending")]
        [MaxLength(25)]
        public string? Status { get; set; }
        
        [Range(10,double.MaxValue)]
        public decimal Price { get; set; }
        [ForeignKey("Users")]
        public required string UserId { get; set; }
        public UserApplication User { get; set; }
        
    }
}