using MailKit.Net.Smtp;
using MimeKit;

namespace ParkIstra.Services.EmailsSender
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public async Task SendEmailAsync(Messages message)
        {
            var mailMessage = CreateEmailMessage(message);
            await SendAsync(mailMessage);
        }

        private MimeMessage CreateEmailMessage(Messages message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("App portal name", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format("<div style='background-color: lightskyblue; padding: 1%; height:50px'><div style='text-align: right; color: white; color: #fdfdfe; font-weight: 600; font-style:italic' ><h3>FUZIP</h3></div></div><div style='margin:1% 4%'><b>{0}</b><br><p>{1}</p><br><a href='{2}'>Kliknite ovde</a><br><br><p><em>{3}</em></p></div>", message.Header, message.Content, message.Link, message.Footer)
            };
            return emailMessage;
        }
        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
