using DriveApp.Core;
using DriveApp.Core.Errors;
using DriveApp.DTO;
using DriveApp.Models.Data;
using DriveApp.Models.Entities;
using DriveApp.Services;
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
        public AccountController(IMemoryCache cache,IEmailSender emailSender,AppDbContext context , UserManager<UserApplication> userManager,IAccountServices accountServices)
        {
            this.userManager = userManager;
            this.accountServices = accountServices;
        }
        [HttpPost("Regestration")]
        public async Task<IActionResult> Regestration(Regestration dto)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(dto.Email);
                if (user is not null)
                {
                    return BadRequest("Email Already Used");
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
                    return Ok(new { userDto, token = tokenData, Expiration = token.ValidTo });
                }
                return BadRequest(result.Errors);
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
        public async Task<IActionResult> ForgetPassword([FromBody][EmailAddress] string email)
        {
            if (ModelState.IsValid)
            {
                var result = await accountServices.ForgetPassword(email);
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
