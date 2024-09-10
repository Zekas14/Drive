using Models.Entities;

namespace DriveApp.Models.Entities
{
    public class Driver :UserApplication
    {       
        public string LicenseNumber { get; set; }
        public string AvailabilityStatus { get; set; }
        public ICollection<TripDetail> tripDetails { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
