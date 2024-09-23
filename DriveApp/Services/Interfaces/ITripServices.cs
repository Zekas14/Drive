using DriveApp.Core.Errors;
using DriveApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DriveApp.Services
{
    public interface ITripDetailServices
    {
        TripDetail GetTripDetails(int id);
        DbSet<TripDetail> GetAll();
    }


    public interface ITripServices
    {
        public DbSet<Trip> GetAllTrips();
        public void AddTrip(Trip trip);
        public Task<ApiResponse> CancelTrip(int id);
        public Task<ApiResponse> AcceptTrip(AcceptTripDto dto); 
        public ApiResponse GetRequestedTrips();
        public Task<ApiResponse> InfromTraveller(int tripId);
        public Task<ApiResponse> AddToTripDetails(TripDetailsDto dto);
        public ApiResponse GetTravellerTrips(string travellerId);
        public ApiResponse RequestTrip(TripDto tripDto);
    }
}
