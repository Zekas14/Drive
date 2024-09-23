using DriveApp.Core.Errors;
using DriveApp.DTO;

namespace DriveApp.Services.Interfaces
{
    public interface IRatingServices
    {
        public ApiResponse RateDriver(RateDriverDto dto);
    }
}
