using API_ADOPTAPATAS_3.Dtos.DtoUser;
using API_ADOPTAPATAS_3.Dtos.RequestUser;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Repositories.Models;
using API_ADOPTAPATAS_3.Utility;
using Microsoft.EntityFrameworkCore;

namespace API_ADOPTAPATAS_3.Repositories.Repository
{
    public class UserRepository
    {
        private readonly BdadoptapatasContext _dbContext;
        private readonly Encrip _encrip;

        public UserRepository(BdadoptapatasContext dbContext, Encrip encrip)
        {
            _dbContext = dbContext;
            _encrip = encrip;
        }

        public async Task<ResponseLoginDto> ObtenerRolIdUsuarioAsync(ReqLoginDto loginDto)
        {
            var login = await _dbContext.Logins.FirstOrDefaultAsync(u => u.Usuario == loginDto.Usuario);

            if (login == null || !_encrip.VerifyPassword(loginDto.Contrasena, login.Contrasena))
            {
                return new ResponseLoginDto { Respuesta = 0 }; // Usuario no existe o contraseña incorrecta
            }

            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.FkLogin == login.IdLogin);
            var fundacion = await _dbContext.Fundacions.FirstOrDefaultAsync(f => f.FkLogin == login.IdLogin);

            if (usuario != null)
            {
                return new ResponseLoginDto
                {
                    Respuesta = 1,
                    IdRol = usuario.FkRol,
                    IdUsuario = usuario.IdUsuario,
                    Nombre = usuario.Nombre, // Ajusta según las propiedades de tu entidad Usuario
                    Correo = usuario.Correo // Ajusta según las propiedades de tu entidad Usuario
                };
            }
            else if (fundacion != null)
            {
                return new ResponseLoginDto
                {
                    Respuesta = 1,
                    IdRol = fundacion.FkRol,
                    IdUsuario = fundacion.IdFundacion, // Considera si quieres que sea IdUsuario o IdFundacion
                    Nombre = fundacion.NombreFundacion, // Ajusta según las propiedades de tu entidad Fundacion
                    Correo = fundacion.Correo // Ajusta según las propiedades de tu entidad Fundacion
                };
            }

            return new ResponseLoginDto { Respuesta = 0 }; // No se encontró ni usuario ni fundación
        }

        // ... otros métodos del repositorio ...
    

    public async Task<bool> RegistrarUsuarioAsync(ReqRegisterDto registerDto)
        {
            var usuarioExistente = await _dbContext.Usuarios
                .AnyAsync(u => u.Nombre == registerDto.Nombre && u.Correo == registerDto.Correo);
            if (usuarioExistente)
            {
                return false; // Nombre de usuario duplicado
            }

            var nuevaCredencial = new Login
            {
                Usuario = registerDto.Usuario,
                Contrasena = _encrip.HashPassword(registerDto.Contrasena)
            };

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

            _dbContext.Logins.Add(nuevaCredencial);
            await _dbContext.SaveChangesAsync();

            var idLogin = nuevaCredencial.IdLogin;
            nuevoUsuario.FkRol = 1;
            nuevoUsuario.FkEstado = 1;
            nuevoUsuario.FkLogin = idLogin;

            _dbContext.Usuarios.Add(nuevoUsuario);
            await _dbContext.SaveChangesAsync();

            return true; // Registro exitoso
        }

        public async Task<bool> RealizarDonacionAsync(ReqDonacionDto donacionDto)
        {
            var canino = await _dbContext.Caninos.FirstOrDefaultAsync(c =>
                c.Nombre == donacionDto.NombreCanino &&
                c.Edad == donacionDto.EdadCanino &&
                c.Raza == donacionDto.RazaCanino);

            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u =>
                u.Nombre == donacionDto.NombreUsuario &&
                u.Correo == donacionDto.CorreoUsuario);

            if (canino == null || usuario == null)
            {
                return false; // Canino o usuario no encontrado
            }

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

        public async Task<bool> RealizarAdopcionAsync(ReqAdopcionDto adopcionDto)
        {
            var canino = await _dbContext.Caninos.FirstOrDefaultAsync(c =>
                c.Nombre == adopcionDto.NombreCanino &&
                c.Edad == adopcionDto.EdadCanino &&
                c.Raza == adopcionDto.RazaCanino);

            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u =>
                u.Nombre == adopcionDto.NombreUsuario &&
                u.Correo == adopcionDto.CorreoUsuario);

            if (canino == null || usuario == null)
            {
                return false; // Canino o usuario no encontrado
            }

            canino.FkEstado = 2;
            canino.Disponibilidad = false;

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
