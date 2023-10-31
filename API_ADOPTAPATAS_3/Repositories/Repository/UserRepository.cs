using API_ADOPTAPATAS_3.Dtos.DtoUser;
using API_ADOPTAPATAS_3.Dtos.RequestUser;
using API_ADOPTAPATAS_3.Repositories.Models;
using API_ADOPTAPATAS_3.Utility;
using Microsoft.EntityFrameworkCore;

namespace API_ADOPTAPATAS_3.Repositories.Repository
{
    public class UserRepository
    {
        public async Task<bool> ValidarUsuarioAsync(ReqLoginDto loginDto)
        {
            using (var _dbContext = new BdadoptapatasContext())
            {
                var usuario = await _dbContext.Logins.FirstOrDefaultAsync(u => u.Usuario == loginDto.Usuario);

                if (usuario == null)
                {
                    return false; // El usuario no existe
                }

                Encrip _encrip = new Encrip();
                if (!_encrip.VerifyPassword(loginDto.Contrasena, usuario.Contrasena))
                {
                    return false; // Contraseña incorrecta
                }

                return true; // Usuario y contraseña válidos
            }
        }

        public async Task<bool> RegistrarUsuarioAsync(ReqRegisterDto registerDto)
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

        public async Task<bool> RealizarDonacionAsync(ReqDonacionDto donacionDto)
        {
            using (var _dbContext = new BdadoptapatasContext())
            {
                // Buscar el canino por nombre, edad y raza
                var canino = await _dbContext.Caninos.FirstOrDefaultAsync(c =>
                    c.Nombre == donacionDto.NombreCanino &&
                    c.Edad == donacionDto.EdadCanino &&
                    c.Raza == donacionDto.RazaCanino);

                if (canino == null)
                {
                    // Manejar la situación en la que el canino no se encontró (por ejemplo, lanzar una excepción)
                    return false; // No se pudo encontrar el canino
                }

                // Buscar al usuario por nombre
                var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => 
                u.Nombre == donacionDto.NombreUsuario &&
                u.Correo == donacionDto.CorreoUsuario
                );

                if (usuario == null)
                {
                    // Manejar la situación en la que el usuario no se encontró (por ejemplo, lanzar una excepción)
                    return false; // No se pudo encontrar el usuario
                }

                // Crear una entidad de donación y asignar valores
                var nuevaDonacion = new Donacion
                {
                    FechaDonacion = donacionDto.FechaDonacion,
                    Monto = donacionDto.Monto,
                    FkCanino = canino.IdCanino,
                    FkUsuario = usuario.IdUsuario
                };

                _dbContext.Donacions.Add(nuevaDonacion);

                await _dbContext.SaveChangesAsync();

                return true; // Donación guardada con éxito
            }
        }

        public async Task<bool> RealizarAdopcionAsync(ReqAdopcionDto adopcionDto)
        {
            using (var _dbContext = new BdadoptapatasContext())
            {
                // Buscar el canino por nombre, edad y raza
                var canino = await _dbContext.Caninos.FirstOrDefaultAsync(c =>
                    c.Nombre == adopcionDto.NombreCanino &&
                    c.Edad == adopcionDto.EdadCanino &&
                    c.Raza == adopcionDto.RazaCanino);

                if (canino == null)
                {
                    // Manejar la situación en la que el canino no se encontró (por ejemplo, lanzar una excepción)
                    return false; // No se pudo encontrar el canino
                }

                // Buscar al usuario por nombre
                var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u =>
               u.Nombre == adopcionDto.NombreUsuario &&
               u.Correo == adopcionDto.CorreoUsuario
               );

                if (usuario == null)
                {
                    // Manejar la situación en la que el usuario no se encontró (por ejemplo, lanzar una excepción)
                    return false; // No se pudo encontrar el usuario
                }

                // Crear una entidad de adopción y asignar valores
                var nuevaAdopcion = new Adopcion
                {
                    FechaAdopcion = adopcionDto.FechaAdopcion,
                    FkCanino = canino.IdCanino,
                    FkUsuario = usuario.IdUsuario
                };

                _dbContext.Adopcions.Add(nuevaAdopcion);

                await _dbContext.SaveChangesAsync();

                return true; // Adopción realizada con éxito
            }
        }


    }
}
