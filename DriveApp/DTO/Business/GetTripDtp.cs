using System.ComponentModel.DataAnnotations.Schema;

namespace DriveApp.DTO
{
    [NotMapped]
    public class GetRequestedTripDto 
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Destination { get; set; }
        public string UserName { get; set; }
        public decimal Price { get; set; } 
    }
}
