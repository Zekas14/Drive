using DriveApp.Core.Errors;
using DriveApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace DriveApp.Services
{
    public interface ITripServices
    {
        public Task<ApiResponse> CancelTrip(int id);
        public Task<ApiResponse> AcceptTrip(AcceptTripDto dto); 
        public ICollection<GetRequestedTripDto> GetTrips();
       // public ApiResponse InfromTraveller(InformTravellerDto dto);
        public Task<ApiResponse> AddToTripDetails(TripDetailsDto dto);
        public ApiResponse RequestTrip(TripDto tripDto);
    }
}
