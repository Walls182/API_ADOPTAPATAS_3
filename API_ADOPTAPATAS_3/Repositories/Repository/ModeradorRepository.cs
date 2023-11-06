using API_ADOPTAPATAS_3.Dtos.RequestModerador;
using API_ADOPTAPATAS_3.Dtos.Responses;
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

        public async Task<ResponseActivarFunDto> ActivarFundacionAsync(ReqActualizarFundacionDto id)
        {
            Encrip _encrip = new Encrip();
            GenericPass genericPass = new GenericPass();
            ResponseActivarFunDto activationResult = new ResponseActivarFunDto();

            using (var _dbContext = new BdadoptapatasContext())
            {
                var fundacion = await _dbContext.Fundacions.FindAsync(id);

                if (fundacion == null)
                {
                    return null; // Fundación no encontrada
                }

                string usuario = fundacion.NombreRepresentante;
                string contrasena = genericPass.GenerateRandomPassword();

                var nuevaCredencial = new Login
                {
                    Usuario = usuario,
                    Contrasena = _encrip.HashPassword(contrasena)
                };

                _dbContext.Logins.Add(nuevaCredencial);
                await _dbContext.SaveChangesAsync();

                fundacion.FkLogin = nuevaCredencial.IdLogin;
                fundacion.FkEstado = 1;
                await _dbContext.SaveChangesAsync();

                // Configura los datos en el objeto activationResult
                activationResult.Usuario = usuario;
                activationResult.Contrasena = contrasena;
                activationResult.Correo = fundacion.Correo;

                return activationResult; // Devuelve los datos de activación
            }
        }


    }
}
