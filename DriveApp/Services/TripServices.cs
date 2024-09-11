using DriveApp.Core.Errors;
using DriveApp.DTO;
using DriveApp.Models.Data;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DriveApp.Services
{
    public class TripServices(AppDbContext context) : ITripServices
    {
        private readonly AppDbContext context = context;

        public ApiResponse RequestTrip(TripDto tripDto)
        {
            try
            {

                Trip trip = new()
                {
                    From = tripDto.UserLocation,
                    To = tripDto.Destination,
                    Status = tripDto.Status,
                    Price = tripDto.Price,
                   UserId = tripDto.UserId,
                };
                context.Trips.Add(trip);
                context.SaveChanges();
                return new ApiResponse(201,"Request Success");
            }catch (Exception e)
            {
                return new ApiResponse(500,e.Message);
            }
        }
        
        public ICollection<GetRequestedTripDto> GetTrips()
        {
            try
            {
                List<GetRequestedTripDto> tripDtos = new();
                var trips = context.Trips.Include(t => t.User).Where(t=>t.Status=="Pending");
                foreach (var item in trips)
                {
                    GetRequestedTripDto tripDto = new()
                    {
                        Id = item.Id,
                        UserName = item.User.UserName?? "Geust",
                        Location = item.From,
                        Destination = item.To,
                        Price = item.Price
                    };
                    tripDtos.Add(tripDto);
                }
                return tripDtos;
            }
            catch
            {
                throw new Exception();
            }
        }
        public async Task<ApiResponse> AddToTripDetails(TripDetailsDto dto)
        {
            try
            {
                var tripDetail = new TripDetail()
                {
                   DateByHour = dto.DatebyHour,
                   DriverId = dto.DriverId,
                   TripId= dto.TripId,
                };
                await context.TripDetails.AddAsync(tripDetail);
                await context.SaveChangesAsync();
                return new ApiResponse(201,"Created");
            }
            catch(Exception e)
            {
                return new ApiResponse(500, e.Message);
            }
        }
        public async Task<ApiResponse> AcceptTrip(AcceptTripDto dto)
        {
            try
            {
                var trip = await context.Trips.FindAsync(dto.TripId);
                if(trip is not null)
                {
                    if (trip.Status!.Equals("Pending"))
                    {
                        var DetailsDto = new TripDetailsDto()
                        {
                            DriverId = dto.DriverId,
                            TripId = dto.TripId,
                            DatebyHour = DateTime.Now
                        };
                        await AddToTripDetails(DetailsDto);
                        context.Trips.Where(t => t.Id == trip.Id).ExecuteUpdate(t => t.SetProperty(t => t.Status, "Accepted"));
                        return new ApiResponse(203, "Trip Accepted");
                    }
                    return new ApiResponse(400, "Trip Not Available");
                }
                return new ApiResponse(404,"Trip not Found");
            }catch(Exception e)
            {
                return new ApiResponse(500,e.Message);
            }
        }
    
        public async Task<ApiResponse> CancelTrip(int id)
        {
            try
            {
                var trip = await context.Trips.FindAsync(id);
                if (trip is not null)
                {
                    if (trip.Status!.Equals("Pending"))
                    {
                        context.Trips.Where(t => t.Id == trip.Id).ExecuteUpdate(t => t.SetProperty(t => t.Status, "Pending"));
                        return new ApiResponse(203, "Trip Cancelled");
                    }
                    return new ApiResponse(400, "Trip Not Available");
                }
                return new ApiResponse(404, "Trip not Found");
            }
            catch (Exception e)
            {
                return new ApiResponse(500,e.Message);
            }
        }

        public async Task<ApiResponse> InfromTraveller(int tripId)
        {
            try
            {
                Trip? trip = context.Trips.FirstOrDefault(t=>t.Id==tripId);
                if (trip is  null)
                {
                    return new ApiResponse(404, "Trip Not Found");
                }
                if (!trip.Status!.Equals("Accepted"))
                {
                    return new ApiResponse(400, "Trip Don't Accepted");

                }
                var driver = await context.TripDetails.Include(d => d.Driver)
                    .Include(t => t.Trip)
                    .Select(d=> new
                    {
                        d.TripId,
                       DriverName = d.Driver.UserName,
                        d.Driver.PhoneNumber,
          
                    })
                    .FirstOrDefaultAsync(t=>t.TripId==tripId);
                InformTravellerDto data = new()
                {
                    DriverName = driver!.DriverName!,
                    PhoneNumber = driver.PhoneNumber!,
                };
                return new ApiResponse(200,"Traveller Informed",data);
            }
            catch (Exception e)
            {
                return new ApiResponse(500, e.Message);
            }
        }
    }
}
