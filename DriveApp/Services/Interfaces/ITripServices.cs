using DriveApp.Core.Errors;
using DriveApp.DTO;
using Models.Entities;

namespace DriveApp.Services
{
    public interface ITripServices
    {
        public Task<ApiResponse> CancelTrip(int id);
        public Task<ApiResponse> AcceptTrip(int tripId); 
        public ICollection<GetRequestedTripDto> GetTrips();
        public ApiResponse RequestTrip(TripDto tripDto);
    }
}
