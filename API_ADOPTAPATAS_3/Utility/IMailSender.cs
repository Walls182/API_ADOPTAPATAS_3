using System.Security.Cryptography.Pkcs;

namespace API_ADOPTAPATAS_3.Utility
{
    public interface IMailSender
    {
       public Task SendEmailHtmlAsync(string email, string asunto, string mensaje);
    }
}
