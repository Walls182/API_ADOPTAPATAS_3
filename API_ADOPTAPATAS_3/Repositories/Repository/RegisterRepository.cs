using API_ADOPTAPATAS_3.Dtos;
using API_ADOPTAPATAS_3.Repositories.Models;
using API_ADOPTAPATAS_3.Utility;

namespace API_ADOPTAPATAS_3.Repositories.Repository
{
    public class RegisterRepository
    {

        public bool RegistrarUsuario(RequestRegisterDto registerDto)
        {
            Encrip _encrip = new Encrip();
            BdadoptapatasContext _dbContext = new BdadoptapatasContext();

            // Verificar si el nombre de usuario ya existe
            if (_dbContext.Logins.Any(u => u.Usuario == registerDto.Usuario))
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
                Departamento = registerDto.Departamento,
                FkLogin = nuevaCredencial.IdLogin, // Establece la referencia a la credencial
                FkRol = nuevoRol.IdRol, // Establece la referencia al rol
                FkEstado = nuevoEstado.IdEstado // Establece la referencia al estado
            };



            // Establecer relaciones
            _dbContext.Logins.Add(nuevaCredencial);
            _dbContext.Rols.Add(nuevoRol);
            _dbContext.Estados.Add(nuevoEstado);
            _dbContext.SaveChanges();

            nuevoUsuario.FkLogin = nuevaCredencial.IdLogin;
            nuevoUsuario.FkRol = nuevoRol.IdRol;
            nuevoUsuario.FkEstado = nuevoEstado.IdEstado;
            _dbContext.Usuarios.Add(nuevoUsuario);
            _dbContext.SaveChanges();
            return true; // Registro exitoso  
        }

    }
}
