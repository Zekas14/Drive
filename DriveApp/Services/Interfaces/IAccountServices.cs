using DriveApp.Core.Errors;
using DriveApp.DTO;

namespace DriveApp.Services
{
    public interface IAccountServices
    {
        public Task<ApiResponse> Login(Login dto);
        public Task<ApiResponse> Register(Regestration dto);
        public Task<ApiResponse> ForgetPassword(string email);
        public Task<ApiResponse> ResetPassword(ResetPasswordDto dto);
    }
}
