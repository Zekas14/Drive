using DriveApp.Core.Errors;
using DriveApp.DTO;
using DriveApp.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;

namespace DriveApp.Services
{
    public class TripServices(AppDbContext context) : ITripServices
    {
        private readonly AppDbContext context = context;
        public DbSet<Trip> GetAllTrips()=>context.Trips;
        public void AddTrip(Trip trip) {
            context.Add(trip);
            context.SaveChanges();
        }

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
                AddTrip(trip);
                return new ApiResponse(201,"Request Success");
            }catch (Exception e)
            {
                return new ApiResponse(500,e.Message);
            }
        }
        
        public ApiResponse GetRequestedTrips()
        {
            try
            {
                List<GetRequestedTripDto> tripDtos = new();
                var trips = GetAllTrips().Include(t => t.User).Where(t=>t.Status=="Pending");
                if (trips.IsNullOrEmpty())
                {
                    return new ApiResponse(404, "No Trips Found");

                }
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
                return new ApiResponse(200,trips);
            }
            catch(Exception e)
            {
                return new(500, e.Message);
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
                var trip = await GetAllTrips().FindAsync(dto.TripId);
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
                        GetAllTrips().Where(t => t.Id == trip.Id).ExecuteUpdate(t => t.SetProperty(t => t.Status, "Accepted"));
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
                var trip = await GetAllTrips().FindAsync(id);
                if (trip is not null)
                {
                    if (trip.Status!.Equals("Pending"))
                    {
                        GetAllTrips().Where(t => t.Id == trip.Id).ExecuteUpdate(t => t.SetProperty(t => t.Status, "Pending"));
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
                Trip? trip = GetAllTrips().FirstOrDefault(t=>t.Id==tripId);
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

        public ApiResponse GetTravellerTrips(string travellerId)
        {
            try
            {
               
                var trip = context.TripDetails.Include(t => t.Trip)
                    .Include(t => t.Driver)
                    .Where(t => t.Trip.UserId == travellerId)
                    .Select(t => new
                    {
                        DriverName = t.Driver.UserName,
                        t.Trip.To,
                        t.Trip.From,
                        Cost = t.Trip.Price,
                        Date = t.DateByHour.ToString("yyyy-mm-dd,U")
                    }
                    );
                if (trip is not null)
                {
                    List<TravellerTripDto> dto = new();
                    foreach (var item in trip)
                    {
                        TravellerTripDto tripDto = new()
                        {
                            DriverName = item.DriverName!,
                            From = item.From,
                            To = item.To,
                            Cost = item.Cost,
                            Date = item.Date
                        };
                        dto.Add(tripDto);
                    }
                    return new ApiResponse(200, dto);
                }

                    return new ApiResponse(404, "Trip Not Found");
            }
            catch (Exception e)
            {
                return new ApiResponse(500, e.Message);
            }
        }

        
    }
}
