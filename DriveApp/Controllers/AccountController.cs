using DriveApp.DTO;
using DriveApp.DTO.Auth;
using DriveApp.Models.Data;
using DriveApp.Models.Entities;
using DriveApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DriveApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {   
        private readonly IAccountServices accountServices;
        public AccountController(IMemoryCache cache,IAccountServices accountServices)
        {
            this.accountServices = accountServices;
        }
        
        [HttpPost("Regestration")]
        public async Task<IActionResult> Regestration(Regestration dto)
        {
            if (ModelState.IsValid)
            {
               var result = await accountServices.Register(dto);
                if(result.StatusCode== 400)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
                return BadRequest(ModelState);
        }
        [HttpPost("DriverRegistration")]
        public async Task<IActionResult> DriverRegestration(Regestration dto)
        {
            if (ModelState.IsValid)
            {
                var result = await accountServices.DriverRegister(dto);
                if (result.StatusCode == 400)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var result = await accountServices.Login(userLoginDto);
                if (result.StatusCode == 400)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await accountServices.ForgetPassword(dto);
                return Ok(result);

            }
            return BadRequest(ModelState);
        }
        [HttpPost("VerifyOtp")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await accountServices.VerifyOtp(dto);
                if (result.StatusCode == 404)
                {
                    return NotFound(result);
                }if (result.StatusCode == 400)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }

                return BadRequest(ModelState);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await accountServices.ResetPassword(dto);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
    }
}
