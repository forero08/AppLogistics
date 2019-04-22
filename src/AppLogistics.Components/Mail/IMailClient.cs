using System.Threading.Tasks;

namespace AppLogistics.Components.Mail
{
    public interface IMailClient
    {
        Task SendAsync(string email, string subject, string body);
    }
}
