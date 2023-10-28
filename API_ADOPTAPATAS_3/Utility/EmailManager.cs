using System.Globalization;
using System.Net;
using System.Net.Mail;

namespace API_ADOPTAPATAS_3.Utility
{
    public class EmailManager
    {
        private SmtpClient Cliente;
        private MailMessage Mensaje;
        private string Host = "smpt.gmail.com";
        private int Port = 465;
        private string User = "adoptapatasconect.app@gmail.com";
        private string Password = "ximxikcjwyycqcmb";// contraseña de aplicacion ximx ikcj wyyc qcmb
        private bool EnableSSL = true;

        public EmailManager() {

            Cliente = new SmtpClient(Host, Port)
            {
                EnableSsl = EnableSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(User, Password)
            };
        }
        public void EnviarCorreo(string destinatario, string asunto, string mensaje, bool esHtml = false)
        {
            Mensaje = new MailMessage(User, destinatario, asunto, mensaje);
            Mensaje.IsBodyHtml = esHtml;
            Cliente.Send(Mensaje);
        }

    }
}
