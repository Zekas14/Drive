using DriveApp.Core.Errors;
using DriveApp.DTO;
using DriveApp.DTO.Auth;

namespace DriveApp.Services
{
    public interface IAccountServices
    {
        public Task<ApiResponse> Login(Login dto);
        public Task<ApiResponse> DriverRegister(Regestration dto);
        public Task<ApiResponse> Register(Regestration dto);
        public Task<ApiResponse> ForgetPassword(ForgetPasswordDto dto);
        public Task<ApiResponse> VerifyOtp(VerifyOtpDto dto);
        public Task<ApiResponse> ResetPassword(ResetPasswordDto dto);
    }
}
