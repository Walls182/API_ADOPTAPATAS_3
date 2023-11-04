using API_ADOPTAPATAS_3.Dtos.RequestModerador;
using API_ADOPTAPATAS_3.Repositories.Models;
using API_ADOPTAPATAS_3.Utility;

namespace API_ADOPTAPATAS_3.Repositories.Repository
{
    public class ModeradorRepository
    {

        public async Task<bool> CambiarRolUsuarioAsync(ReqCambioRolDto rol)
        {
            using (var _dbContext = new BdadoptapatasContext())
            {
                // Busca el usuario por su ID
                var usuario = await _dbContext.Usuarios.FindAsync(rol.IdUsuario);

                if (usuario == null)
                {
                    return false; // Usuario no encontrado
                }

                // Actualiza el campo FkRol con el nuevo rol
                usuario.FkRol = rol.IdNuevoRol;

                // Guarda los cambios en la base de datos
                await _dbContext.SaveChangesAsync();

                return true; // Cambio de rol exitoso
            }
        }
        public async Task<bool> CambiarEstadoUsuarioAsync(ReqCambioEstadoDto estado)
        {
            using (var _dbContext = new BdadoptapatasContext())
            {
                // Busca el usuario por su ID
                var usuario = await _dbContext.Usuarios.FindAsync(estado.Id);

                if (usuario == null)
                {
                    return false; // Usuario no encontrado
                }

                // Actualiza el campo FkEstado con el nuevo estado
                usuario.FkEstado = estado.estado;

                // Guarda los cambios en la base de datos
                await _dbContext.SaveChangesAsync();

                return true; // Cambio de estado exitoso
            }
        }

        public async Task<bool> ActivarFundacionAsync(ReqActualizarFundacionDto id)
        {
            Encrip _encrip = new Encrip();
            GenericPass genericPass = new GenericPass();
            EmailManager emailManager = new EmailManager();
            using (var _dbContext = new BdadoptapatasContext())
            {

                // Busca la fundación por su ID
                var fundacion = await _dbContext.Fundacions.FindAsync(id);

                if (fundacion == null)
                {
                    return false; // Fundación no encontrada
                }

                // Genera usuario y contraseña para la fundación
                string usuario = fundacion.NombreRepresentante; // Implementa tu lógica para generar usuarios únicos
                string contrasena = genericPass.GenerateRandomPassword(); // Implementa tu lógica para generar contraseñas aleatorias

                // Crea un objeto Login con el usuario y la contraseña
                var nuevaCredencial = new Login
                {
                    Usuario = usuario,
                    Contrasena = _encrip.HashPassword(contrasena)
                };

                _dbContext.Logins.Add(nuevaCredencial);

                // Guarda los cambios en la base de datos para la credencial
                await _dbContext.SaveChangesAsync();

                // Actualiza los valores de la fundación
                fundacion.FkLogin = nuevaCredencial.IdLogin;
                fundacion.FkEstado = 1; // Estado activado

                // Guarda los cambios en la base de datos para la fundación
                await _dbContext.SaveChangesAsync();

                // Envía las credenciales por correo electrónico
                string mensajeCorreo = "<html><body>";
                mensajeCorreo += "<h2>Datos de Inicio de Sesión para tu fundación</h2>";
                mensajeCorreo += "<h2>Tu solicitud de registro en adoptapatas ha sido aprobada. A continuación, te enviamos las credenciales para iniciar sesión en la plataforma.</h2>";
                mensajeCorreo += "<p><strong>Usuario:</strong> " + usuario + "</p>";
                mensajeCorreo += "<p><strong>Contraseña:</strong> " + contrasena + "</p>";
                mensajeCorreo += "<p>¡Gracias por unirte a nuestra plataforma!</p>";
                mensajeCorreo += "</body></html>";

                emailManager.EnviarCorreo(fundacion.Correo, "Datos de Inicio de Sesión en adoptapatas", mensajeCorreo, true);

                return true; // Activación exitosa
            }
        }

    }
}
