using System.Net;
using System.Net.Mail;

namespace API_ADOPTAPATAS_3.Utility
{
    public class EmailSender : IMailSender
    {


        //Version original
     
        //Version con html
        public async Task SendEmailHtmlAsync(string email, string asunto, string mensaje)
        {
            /* var _email = "ADOPTAPATASCONECT@outlook.com";
             var Pass = "ADOPTAPATASAPI3";
             var client = new SmtpClient("", 587)

             */
            
            string _email = "tukodatabases@gmail.com";
            string Pass = "1003534379Ti";
            var client = new SmtpClient("smtp.gmail.com", 587)
          


            {
                UseDefaultCredentials = true,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
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
