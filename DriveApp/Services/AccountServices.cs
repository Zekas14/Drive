using DriveApp.Core;
using DriveApp.Core.Errors;
using DriveApp.DTO;
using DriveApp.DTO.Auth;
using DriveApp.Models.Data;
using DriveApp.Models.Entities;
using DriveApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System.IdentityModel.Tokens.Jwt;
namespace DriveApp.Services
{
    public class AccountServices(UserManager<UserApplication> userManager,AppDbContext dbContext,
        IMailServices mailServices,IMemoryCache cache) : IAccountServices
    {
        private readonly UserManager<UserApplication> userManager = userManager;
        private readonly AppDbContext dbContext = dbContext;
        private readonly IMailServices mailServices = mailServices;
        private readonly IMemoryCache _cache = cache;
        private string GenerateOtp()
        {
            return new Random().Next(1000,10000).ToString();
        }
        public async Task<ApiResponse> ForgetPassword(ForgetPasswordDto dto)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(dto.Email);
                if (user is null)
                {
                    return new ApiResponse(404, "User Not Found");
                }
                var otp = GenerateOtp();
                var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);

                _cache.Set($"otp", otp, TimeSpan.FromMinutes(15));
                _cache.Set($"resetToken", resetToken, TimeSpan.FromMinutes(10));
                try
                {
                    await mailServices.SendEmailAsync(dto.Email, "Otp Verification", $" Your Verification OTP Code is : {otp} ");
                return new ApiResponse(200, "Otp Sent To Your Email");
                }catch(Exception e)
                {
                    return new ApiResponse(500,e.Message);
                }

            }
            catch (Exception e )
            {
                return new ApiResponse(500,e.Message);
            }
        }
        public async Task<ApiResponse> VerifyOtp(VerifyOtpDto dto)
        {
            string? validateToken= null;
            _cache.Set($"validateToken", validateToken, TimeSpan.FromMinutes(15));
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user is not null)
            {
                if (_cache.TryGetValue("otp", out string? validOtp))
                {
                    if (dto.InputOtp == validOtp)
                    {
                        validateToken = _cache.Get<string>("resetToken")!;
                       _cache.Set($"validateToken", validateToken, TimeSpan.FromMinutes(15));
                        return new ApiResponse(200,"OTP Verified");
                    }

                    return new ApiResponse(400, "Invalid OTP");
                }
                return new ApiResponse(400, "OTP Timeout");

            }
            return new ApiResponse(404, "User Not Found");
        }

        public async Task<ApiResponse> ResetPassword(ResetPasswordDto dto)
        {
                var user = await userManager.FindByEmailAsync(dto.Email);
                if (user is not null)
            {
                if (!_cache.TryGetValue("validateToken", out string? validateToken)||validateToken==null)
                {
                    return new ApiResponse(400, "your didn't Verify Email OTP");
                }
                var resetToken = _cache.Get<string>("resetToken");
                if (resetToken == validateToken)
                {
                    var result = await userManager.ResetPasswordAsync(user, resetToken!, dto.NewPassword);
                    if (result.Succeeded)
                    {
                        return new ApiResponse(200,"Password Changed Sucessfully");
                    }
                        
                }       
            }
                return new ApiResponse(404,"User Not Found");
        }
        public async Task<ApiResponse> Login(Login dto)
        {
            try 
            {
                var currentUser = await userManager.FindByEmailAsync(dto.Email);
                if(currentUser is null)
                {
                    return new ApiResponse(404, "User Not Found");
                }
                bool found = await userManager.CheckPasswordAsync(currentUser,dto.Password);
                if (!found)
                {
                    return new ApiResponse(400,"Wrong Email or Password");
                }
                var token = await userManager.CreateTokenAsync(currentUser);
                string tokenData = new JwtSecurityTokenHandler().WriteToken(token);

                UserDto user = new()
                {
                    Name = currentUser.UserName,
                    Email = currentUser.Email,
                    Address = currentUser.Address,
                    Id = currentUser.Id,
                    Phone = currentUser.PhoneNumber,
                };
                return new ApiResponse(200, "Login Success", new { user, token = tokenData, Expiration = token.ValidTo });


            }
            catch (Exception e)
            {
                return new ApiResponse(500,e.Message);
            }
        }

        public async Task<ApiResponse> Register(Regestration dto)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(dto.Email);
                if (user is not null)
                {
                    return new ApiResponse(400, "Email Already Used");
                }
                user = new UserApplication()
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                    Address = dto.Address,
                    PhoneNumber = dto.PhoneNumber,
                };

                IdentityResult result = await userManager.CreateAsync(user, dto.Password);
                if (result.Succeeded)
                {

                    await userManager.AddToRoleAsync(user, dto.Role);
                    var token = await userManager.CreateTokenAsync(user);
                    string tokenData = new JwtSecurityTokenHandler().WriteToken(token);

                    UserDto userDto = new()
                    {
                        Name = user.UserName,
                        Email = user.Email,
                        Address = user.Address,
                        Id = user.Id,
                        Phone = user.PhoneNumber,
                    };
                    return new ApiResponse(200, "Registration Success", new { userDto, token = tokenData, Expiration = token.ValidTo });
                }
                return new ApiResponse(400,"registration Failed",result.Errors);
            }
            catch (Exception e)
            {
                return new ApiResponse(500, e.Message);
            }
        }

    }
}
