using MimeKit;

namespace ParkIstra.Services.EmailsSender
{
    public class Messages
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string? Header { get; set; }
        public string Content { get; set; }
        public string? Link { get; set; }
        public string? Footer { get; set; }
        public string Type { get; set; }
        public Messages(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("", x)));
            Subject = subject;
            Content = content;
        }

        public Messages(IEnumerable<string> to, string subject, string content, string header, string link, string footer)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("", x)));
            Subject = subject;
            Content = content;
            Header = header;
            Link = link;
            Footer = footer;
        }
    }
}
