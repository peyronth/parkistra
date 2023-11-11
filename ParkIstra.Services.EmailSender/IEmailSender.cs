namespace ParkIstra.Services.EmailsSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Messages message);
    }
}
