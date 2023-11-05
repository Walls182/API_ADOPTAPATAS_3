using System.Net;
using System.Net.Mail;

namespace API_ADOPTAPATAS_3.Utility
{
    public class EmailSender : IMailSender
    {


        //Version original
        public Task SendEmailAsync(string email, string asunto, string mensaje)
        {
            var _email = "ADOPTAPATASCONECT@outlook.com";
            var Pass = "ADOPTAPATASAPI3";
            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_email, Pass)
            };
            return client.SendMailAsync(
                new MailMessage(from: _email, to: email, asunto, mensaje));
        }
        //Version con html
        public async Task SendEmailHtmlAsync(string email, string asunto, string mensaje)
        {
            var _email = "ADOPTAPATASCONECT@outlook.com";
            var Pass = "ADOPTAPATASAPI3";
            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_email, Pass)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_email),
                Subject = asunto,
                IsBodyHtml = true // Esta línea habilita HTML en el cuerpo del mensaje
            };
            mailMessage.To.Add(email);

            mailMessage.Body = mensaje; // Establece el cuerpo del mensaje

            await client.SendMailAsync(mailMessage);
        }
    }
}
