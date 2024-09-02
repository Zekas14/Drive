using DriveApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DriveApp.Core
{
    public static class ExtensionFunctions
    {
        public static string GetBaseUrl(this HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host.Value}{request.PathBase.Value}";
        }
        public static async Task<JwtSecurityToken> CreateTokenAsync(this UserManager<UserApplication> userManager, UserApplication user)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName!));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var roles = await userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(config["JWT:SecretKey"]!));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken
                (
                    issuer: config["JWT:Issuer"],
                    audience: config["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCredentials
                );
            return token;
        }
    }
}

