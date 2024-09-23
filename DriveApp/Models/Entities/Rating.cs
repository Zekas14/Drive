using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveApp.Models.Entities
{
    [Table("Ratings")]
    public class Rating
    {
        public int Id { get; set; }
        [MaxLength(5)]
        [MinLength(0)]
        public int RateNumber { get; set; }
        public string? Comment { get;set; }
        [ForeignKey("Drivers")]
        public string DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}
