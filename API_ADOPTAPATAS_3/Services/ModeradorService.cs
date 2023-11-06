
using API_ADOPTAPATAS_3.Dtos.RequestModerador;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Repositories.Models;
using API_ADOPTAPATAS_3.Repositories.Repository;
using API_ADOPTAPATAS_3.Utility;

namespace API_ADOPTAPATAS_3.Services
{
    public class ModeradorService
    {
      private readonly ModeradorRepository _repository;
        private readonly IMailSender _mailSender;

        public ModeradorService(ModeradorRepository repository, IMailSender mailSender) 
        {
        _repository = repository;
            _mailSender = mailSender;

        }
        public async Task<ResponseGeneric> CambioRol(ReqCambioRolDto cambio)
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

        public async Task<ResponseGeneric> CambioEstado(ReqCambioRolDto cambio)
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

                if (activationResult != null)
                {
                    // Envía las credenciales por correo electrónico
                    string mensajeCorreo = "<html><body>";
                    mensajeCorreo += "<h2>Datos de Inicio de Sesión para tu fundación</h2>";
                    mensajeCorreo += "<h2>Tu solicitud de registro en adoptapatas ha sido aprobada. A continuación, te enviamos las credenciales para iniciar sesión en la plataforma.</h2>";
                    mensajeCorreo += "<p><strong>Usuario:</strong> " + activationResult.Usuario + "</p>";
                    mensajeCorreo += "<p><strong>Contraseña:</strong> " + activationResult.Contrasena + "</p>";
                    mensajeCorreo += "<p>¡Gracias por unirte a nuestra plataforma!</p>";
                    mensajeCorreo += "</body></html>";

                  await _mailSender.SendEmailHtmlAsync(activationResult.Correo, "Datos de Inicio de Sesión en adoptapatas", mensajeCorreo);
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

    }
}
