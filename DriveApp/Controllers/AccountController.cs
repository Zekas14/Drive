using DriveApp.Core;
using DriveApp.Core.Errors;
using DriveApp.DTO;
using DriveApp.Models.Data;
using DriveApp.Models.Entities;
using DriveApp.Services;
using DriveApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DriveApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {   private readonly UserManager<UserApplication> userManager;
        private readonly IAccountServices accountServices;
        private readonly IMailServices _emailService;
        public AccountController(IMemoryCache cache,IMailServices emailService,AppDbContext context , UserManager<UserApplication> userManager,IAccountServices accountServices)
        {
            this.userManager = userManager;
            this.accountServices = accountServices;
            this._emailService = emailService;
        }
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendTestEmail(SendEmailDto dto)
        {
            await _emailService.SendEmailAsync(dto.Email, dto.Subject, dto.Body,dto.Attachments);
            return Ok("Email sent successfully.");
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
