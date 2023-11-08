using API_ADOPTAPATAS_3.Dtos.RequestModerador;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Repositories.Models;
using API_ADOPTAPATAS_3.Utility;
using Microsoft.EntityFrameworkCore;

namespace API_ADOPTAPATAS_3.Repositories.Repository
{
    public class ModeradorRepository
    {
        private readonly BdadoptapatasContext _dbContext;
        private readonly Encrip _encrip;
        private readonly GenericPass _genericPass;

        public ModeradorRepository(BdadoptapatasContext dbContext, Encrip encrip, GenericPass genericPass)
        {
            _dbContext = dbContext;
            _encrip = encrip;
            _genericPass = genericPass;
        }

        public async Task<bool> CambiarRolUsuarioAsync(ReqCambioRolDto rol)
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

        public async Task<bool> CambiarEstadoUsuarioAsync(ReqCambioEstadoDto estado)
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

        public async Task<ResponseActivarFunDto> ActivarFundacionAsync(ReqActualizarFundacionDto id)
        {
            var fundacion = await _dbContext.Fundacions.FindAsync(id.Id);

            if (fundacion != null)
            {
                // Genera una nueva contraseña
                var contrasena = _genericPass.GenerateRandomPassword();

                // Busca la entidad Login asociada a la fundación
                var login = await _dbContext.Logins.FindAsync(fundacion.FkLogin);

                if (login != null)
                {
                    // Actualiza las credenciales en la entidad Login
                    login.Usuario = fundacion.NombreRepresentante;
                    login.Contrasena = _encrip.HashPassword(contrasena);
                }

                // Cambia el estado y guarda los cambios
                fundacion.FkEstado = 1;

                await _dbContext.SaveChangesAsync();

                var activationResult = new ResponseActivarFunDto
                {
                    Usuario = fundacion.NombreRepresentante,
                    Contrasena = contrasena,
                    Correo = fundacion.Correo
                };

                return activationResult;
            }

            return null; // Fundación no encontrada
        }


        public async Task<List<Fundacion>> ObtenerFundaciones()
        {
            var fundaciones = await _dbContext.Fundacions.ToListAsync();
            return fundaciones;
        }
    }

}
