using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DriveApp.DTO.Auth
{
    public class SendEmailDto
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public IList<IFormFile>? Attachments { get; set; }
    }
}
