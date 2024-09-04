namespace DriveApp.Services.Interfaces
{
    public interface IMailServices 
    {
        Task SendEmailAsync(string mailTo,string subject,string body , IList<IFormFile>? attachment = null);
    }
}
