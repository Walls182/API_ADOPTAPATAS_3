
using API_ADOPTAPATAS_3.Dtos.RequestModerador;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Repositories.Models;
using API_ADOPTAPATAS_3.Repositories.Repository;
using API_ADOPTAPATAS_3.Utility;
using System.Net.Mail;
using System.Net;
using System.Web.Http;

namespace API_ADOPTAPATAS_3.Services
{
    public class ModeradorService
    {
      private readonly ModeradorRepository _repository;
       

        public ModeradorService(ModeradorRepository repository) 
        {
        _repository = repository;
            

        }
        public async Task<ResponseGeneric> CambioRolService(ReqCambioRolDto cambio)
        {
            try
            {
                var registroExitoso = await _repository.CambiarRolUsuarioAsync(cambio);

                return new ResponseGeneric
                {
                    respuesta = registroExitoso ? 1 : 0,
                };
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error en Cambio de ROl: " + ex.Message);

                // Devolver una respuesta detallada sobre el error
                return new ResponseGeneric
                {
                    respuesta = 0,
                    mensaje = "Error en el Cambio de Rol: " + ex.Message
                };
            }
        }

        public async Task<ResponseGeneric> CambioEstadoService(ReqCambioEstadoDto cambio)
        {
            try
            {
                var registroExitoso = await _repository.CambiarEstadoUsuarioAsync(cambio);

                return new ResponseGeneric
                {
                    respuesta = registroExitoso ? 1 : 0,
                
                };
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error en Cambio de estado: " + ex.Message);

                // Devolver una respuesta detallada sobre el error
                return new ResponseGeneric
                {
                    respuesta = 0,
                    mensaje = "Error en el Cambio de estado: " + ex.Message
                };
            }
        }
        public async Task<ResponseGeneric> ActivarFundacionService(ReqActualizarFundacionDto idfundacion)
        {
            try
            {
                var activationResult = await _repository.ActivarFundacionAsync(idfundacion);
                string email = "tukodatabases@gmail.com";
                string password = "fulscagiehwazjnp";
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;

                if (activationResult != null)
                {
 
                    var client = new SmtpClient(smtpServer, smtpPort)
                    {
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(email, password),
                        EnableSsl = true,
                    };

                    var message = new MailMessage
                        (email, activationResult.Correo,
                        "CREDENCIALES INGRESO aDOPTAPATAS CONECT",
                        "BIENVENIDA" + activationResult.Usuario +
                        "Tu solicitud de registro fue aprobada ," +
                        " disfruta tu experiencia" +
                        "");

                    // Crea el cuerpo del mensaje en formato HTML
                    string body = @"
                                <h1>Bienvenido a Adoptapatas Conect</h1>
                                <p>Gracias por unirte a nuestra comunidad. Estamos emocionados de tenerte como parte de ADOPTAPATASCONNECT.</p>
                                <h2>Tu solicitud de registro en adoptapatas ha sido aprobada. A continuación, te enviamos las credenciales para iniciar sesión en la plataforma.</h2>
                                <img src='https://i.pinimg.com/736x/83/c3/7c/83c37c101f433d7c2eea87a18e3f45b5.jpg' alt='Imagen de bienvenida'>
                                <p><strong>Usuario:</strong> " + activationResult.Usuario +
                                "<p>< strong > Contraseña:</ strong > " + activationResult.Contrasena +
                                "</p><h2>TE DAMOS LAS GRACIAS EN NUESTRO EQUIPO DE TRABAJO</h2>";
                    message.IsBodyHtml = true;
                    message.Body = body;

                    client.Send(message);
                }

                return new ResponseGeneric
                {
                    respuesta = activationResult != null ? 1 : 0,
                };
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)

                // Devolver una respuesta detallada sobre el error
                return new ResponseGeneric
                {
                    respuesta = 0,
                };
            }
        }
        public async Task<List<ResponseListaFundacionesDto>> ObtenerFundaciones()
        {
            // Utiliza el método del repositorio para obtener la lista de fundaciones
            var fundaciones = await _repository.ObtenerFundaciones();

            if (fundaciones != null && fundaciones.Any())
            {
                var fundacionesDtoList = new List<ResponseListaFundacionesDto>();

                foreach (var fundacion in fundaciones)
                {
                    var fundacionDto = new ResponseListaFundacionesDto
                    {
                        IdFundacion = fundacion.IdFundacion,
                        respuesta = 1,
                        NombreFundacion = fundacion.NombreFundacion,
                        NombreRepresentante = fundacion.NombreRepresentante,
                        Direccion = fundacion.Direccion,
                        Municipio = fundacion.Municipio,
                        Departamento = fundacion.Departamento,
                        Correo = fundacion.Correo,
                        Telefono = fundacion.Telefono,
                        Celular = fundacion.Celular,
                        Descripcion = fundacion.Descripcion,
                        Mision = fundacion.Mision,
                        Vision = fundacion.Vision,
                        ObjetivoSocial = fundacion.ObjetivoSocial,
                        LogoFundacion = fundacion.LogoFundacion,
                        FotoFundacion = fundacion.FotoFundacion

                    };

                    fundacionesDtoList.Add(fundacionDto);
                }

                return fundacionesDtoList;
            }
            else
            {
                // Si la lista está vacía o es null, puedes devolver una lista vacía o un mensaje apropiado
                return new List<ResponseListaFundacionesDto>();
            }
        }

    }
}
