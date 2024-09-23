using System.ComponentModel.DataAnnotations;

namespace DriveApp.DTO
{
    public class RateDriverDto
    {
        public string DriverId { get; set; }
        [Range(0, 5)]
        public int Rate { get; set; }
        public string Comment { get; set; }
    }
}
