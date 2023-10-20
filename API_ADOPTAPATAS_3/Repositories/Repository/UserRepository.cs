using API_ADOPTAPATAS_3.Dtos;
using API_ADOPTAPATAS_3.Repositories.Models;
using API_ADOPTAPATAS_3.Utility;
using Microsoft.EntityFrameworkCore;

namespace API_ADOPTAPATAS_3.Repositories.Repository
{
    public class UserRepository
    {
        public async Task<bool> ValidarUsuarioAsync(RequestLoginDto loginDto)
        {
            Encrip _encrip = new Encrip();
            using (var _dbContext = new BdadoptapatasContext())
            {
                var passs = _encrip.HashPassword(loginDto.Contrasena);
                var usuario = await _dbContext.Logins.FirstOrDefaultAsync(u => u.Usuario == loginDto.Usuario);

                if (usuario == null || usuario.Contrasena != passs)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public async Task<bool> RegistrarUsuarioAsync(RequestRegisterDto registerDto)
        {
            Encrip _encrip = new Encrip();
            using (var _dbContext = new BdadoptapatasContext())
            {
                // Verificar si el nombre de usuario ya existe
                if (await _dbContext.Logins.AnyAsync(u => u.Usuario == registerDto.Usuario))
                {
                    return false; // Nombre de usuario duplicado
                }

                var nuevaCredencial = new Login
                {
                    Usuario = registerDto.Usuario,
                    Contrasena = _encrip.HashPassword(registerDto.Contrasena)
                };

                var nuevoRol = new Rol
                {
                    Nombre = "Normal"
                };

                var nuevoEstado = new Estado
                {
                    DescripEstado = "Activo"
                };

                // Crear una entidad de usuario y asignar valores
                var nuevoUsuario = new Usuario
                {
                    Nombre = registerDto.Nombre,
                    Apellido = registerDto.Apellido,
                    Correo = registerDto.Correo,
                    Celular = registerDto.Celular,
                    Direccion = registerDto.Direccion,
                    Municipio = registerDto.Municipio,
                    Departamento = registerDto.Departamento
                };

                // Establecer relaciones
                _dbContext.Logins.Add(nuevaCredencial);
                _dbContext.Rols.Add(nuevoRol);
                _dbContext.Estados.Add(nuevoEstado);

                await _dbContext.SaveChangesAsync();

                // Obtener el IdLogin después de guardar los cambios
                var idLogin = nuevaCredencial.IdLogin;

                nuevoUsuario.FkLogin = idLogin;
                nuevoUsuario.FkRol = nuevoRol.IdRol;
                nuevoUsuario.FkEstado = nuevoEstado.IdEstado;

                _dbContext.Usuarios.Add(nuevoUsuario);

                await _dbContext.SaveChangesAsync();

                return true; // Registro exitoso
            }
        }

    }
}
