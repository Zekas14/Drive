using Models.Entities;

namespace DriveApp.Models.Entities
{
    public class Traveller :UserApplication
    {

        public ICollection<Trip> Trips { get; set; } = new List<Trip>();

    }
}
