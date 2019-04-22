using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AppLogistics.Components.Mail
{
    public class SmtpMailClient : IMailClient
    {
        private IConfiguration Config { get; }

        public SmtpMailClient(IConfiguration config)
        {
            Config = config.GetSection("Mail");
        }

        public async Task SendAsync(string email, string subject, string body)
        {
            using (SmtpClient client = new SmtpClient(Config["Host"], int.Parse(Config["Port"])))
            {
                client.Credentials = new NetworkCredential(Config["Sender"], Config["Password"]);
                client.EnableSsl = bool.Parse(Config["EnableSsl"]);

                MailMessage mail = new MailMessage(Config["Sender"], email, subject, body);
                mail.SubjectEncoding = Encoding.UTF8;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;

                await client.SendMailAsync(mail);
            }
        }
    }
}
