using System.Threading.Tasks;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Templating;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace CoachTwinsApi.Logic.Mail
{
    public class SmtpMailer: IMailer
    {
        private readonly TemplateRenderer _mailRenderer;
        private readonly SmtpClient _client;
        private readonly MailboxAddress _from;

        public SmtpMailer(TemplateRenderer mailRenderer)
        {
            _mailRenderer = mailRenderer;
            _client = new SmtpClient();
            _from = new MailboxAddress("Coach Twins", "testing@coachtwins.nl");
        }
        
        public async Task SendMail<T>(User to, string subject, string template, T data)
        {
            var toAddress = new MailboxAddress(to.FirstName, to.Email);
            var message = new MimeMessage();
            message.From.Add(_from);
            message.To.Add(toAddress);

            var content = await _mailRenderer.Render(template, data);
            
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = content
            };
            message.Subject = subject;

            await _client.ConnectAsync("localhost", 25);

            await _client.SendAsync(message);

            await _client.DisconnectAsync(true);
        }

        public async Task SendMail(User to, string subject, string template)
        {
            await SendMail<string>(to, subject, template, default);
        }

        ~SmtpMailer()
        {
            _client.Dispose();
        }
    }
}