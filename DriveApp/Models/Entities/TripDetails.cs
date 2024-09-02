using DriveApp.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class TripDetail
    {
        public int Id { get;set; }  
        [ForeignKey("Trips")]
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        [ForeignKey("Drivers")]
        public string DirverId { get; set; }
        public Driver Driver { get; set; }
        public DateTime DateByHour { get; set; } =DateTime.Now;
    }
}