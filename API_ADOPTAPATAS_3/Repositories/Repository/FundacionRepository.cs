using API_ADOPTAPATAS_3.Dtos.RequestCanino;
using API_ADOPTAPATAS_3.Dtos.RequestFundacion;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Repositories.Models;
using API_ADOPTAPATAS_3.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_ADOPTAPATAS_3.Repositories.Repository
{
    public class FundacionRepository
    {
        BdadoptapatasContext _dbContext = new BdadoptapatasContext();
        EmailManager _emailManager = new EmailManager();
        Encrip _encrip = new Encrip();
        GenericPass GenericPass = new GenericPass();




        public async Task<bool> RegistrarFundacionAsync(ReqRegistroFundDto fundacionDto)
        {
            using (var _dbContext = new BdadoptapatasContext())
            {
                // Verifica si la fundación ya está registrada
                if (await _dbContext.Fundacions
                    .AnyAsync(f => f.NombreFundacion == fundacionDto.NombreFundacion && f.Correo == fundacionDto.Correo))
                {
                    return false; // Fundación ya registrada
                }

                // Genera usuario y contraseña para la fundación
                string usuario = fundacionDto.NombreRepresentante; // Implementa tu lógica para generar usuarios únicos
                string contrasena = GenericPass.GenerateRandomPassword(); // Implementa tu lógica para generar contraseñas aleatorias

                // Crea un objeto Fundacion con los datos proporcionados
                var nuevaFundacion = new Fundacion
                {
                    NombreRepresentante = fundacionDto.NombreRepresentante,
                    NombreFundacion = fundacionDto.NombreFundacion,
                    Direccion = fundacionDto.Direccion,
                    Municipio = fundacionDto.Municipio,
                    Departamento = fundacionDto.Departamento,
                    Correo = fundacionDto.Correo,
                    Telefono = fundacionDto.Telefono,
                    Celular = fundacionDto.Celular,
                    Descripcion = fundacionDto.Descripcion,
                    Mision = fundacionDto.Mision,
                    Vision = fundacionDto.Vision,
                    ObjetivoSocial = fundacionDto.ObjetivoSocial,
                    LogoFundacion = fundacionDto.LogoFundacion,
                    FotoFundacion = fundacionDto.FotoFundacion,
                    FkRol = 2, // Rol predefinido
                    FkEstado = 2, // Estado predefinido
                };

                // Crea un objeto Login con el usuario y la contraseña
                var nuevaCredencial = new Login
                {
                    Usuario = usuario,
                    Contrasena = _encrip.HashPassword(contrasena)
                };

                _dbContext.Fundacions.Add(nuevaFundacion);
                _dbContext.Logins.Add(nuevaCredencial);

                // Guarda los cambios en la base de datos
                await _dbContext.SaveChangesAsync();

                // Asigna el ID de la nueva credencial a la fundación
                nuevaFundacion.FkLogin = nuevaCredencial.IdLogin;

                // Guarda los cambios nuevamente con el FkLogin actualizado
                await _dbContext.SaveChangesAsync();

                // Envía el usuario y la contraseña por correo electrónico
                string mensajeCorreo = "<html><body>";
                mensajeCorreo += "<h2>Datos de Inicio sesión para tu fundación</h2>";
                mensajeCorreo += "<h2>Tu solicitud de registro en adoptapatas fue aceptada, a continuacion te enviavos las credenciales para tu inicio de sesion</h2>";
                mensajeCorreo += "<p><strong>Usuario:</strong> " + usuario + "</p>";
                mensajeCorreo += "<p><strong>Contraseña:</strong> " + contrasena + "</p>";
                mensajeCorreo += "<p>¡Gracias por registrarte en nuestra plataforma!</p>";
                mensajeCorreo += "</body></html>";

                _emailManager.EnviarCorreo(fundacionDto.Correo, "Datos de Inicio sesión en adoptapatas", mensajeCorreo, true);

                return true; // Registro exitoso
            }
        }


        public async Task<bool> ExistCanino(ReqCrearCaninoDto canino)
        {
            if (await _dbContext.Caninos.AnyAsync
                (
                u => u.Nombre == canino.Nombre &&
                u.Raza == canino.Raza &&
                u.Edad == canino.Edad
            ))
            {
                return true; // canino existe
            }

            return false;

        }
        public async Task<Canino> BuscarCanino(ReqBuscarCaninoDto find)
        {
            // Realiza la búsqueda en la base de datos
            var canino = await _dbContext.Caninos.FirstOrDefaultAsync(u =>
                u.Nombre == find.Nombre &&
                u.Raza == find.Raza &&
                u.Edad == find.Edad);

            if (canino == null)
            {
                return null;// Devuelve una respuesta indicando que no se encontró ningún canino

            }

            // Devuelve el canino encontrado
            return canino;
        }
        public async Task<bool> CrearCaninoAsync(ReqCrearCaninoDto caninoDto)
        {
            // Verificar si el canino ya existe
            if (await ExistCanino(caninoDto))
            {
                return false; // Canino existe
            }

            var nuevaMascota = new Canino
            {
                Nombre = caninoDto.Nombre,
                Edad = caninoDto.Edad,
                Raza = caninoDto.Raza,
                Descripcion = caninoDto.Descripcion,
                Imagen = caninoDto.Imagen,
                EstadoSalud = caninoDto.EstadoSalud,
                Temperamento = caninoDto.Temperamento,
                Vacunas = caninoDto.Vacunas,
                Disponibilidad = caninoDto.Disponibilidad,
                FkFundacion = caninoDto.FkFundacion
                
        };
                nuevaMascota.FkEstado = 1;
            _dbContext.Caninos.Add(nuevaMascota);
            await _dbContext.SaveChangesAsync();

            return true; // Creación exitosa de la mascota
        }

        public async Task<bool> ActualizarCaninoAsync(ReqActualizarCaninoDto mascotaDto)
        {
            // Buscar el canino en la base de datos por su ID
            var canino = await _dbContext.Caninos.FindAsync(mascotaDto.IdCanino);

            if (canino == null)
            {
                return false; // El canino no se encontró, la actualización no es posible
            }

            // Actualizar el canino con las propiedades del DTO
            _dbContext.Entry(canino).CurrentValues.SetValues(mascotaDto);

            // Guardar los cambios en la base de datos
            await _dbContext.SaveChangesAsync();

            return true; // Actualización exitosa del canino
        }

        public async Task<List<Canino>> ObtenerCaninosDisponiblesAsync()
        {
            // Realiza una consulta para obtener todos los caninos con disponibilidad en true
            var caninosDisponibles = await _dbContext.Caninos
                .Where(c => c.Disponibilidad == true)
                .ToListAsync();

            return caninosDisponibles;
        }
        public async Task<List<Canino>> ObtenerCaninosPorFundacionAsync(ReqIdFunDto idFundacion)
        {
            // Realiza una consulta para obtener todos los caninos asociados a una fundación específica
            var caninosPorFundacion = await _dbContext.Caninos
                .Where(c => c.FkFundacion == idFundacion.IdFundacion)
                .ToListAsync();

            return caninosPorFundacion;
        }
        public async Task<bool> ActualizarDisponibilidad(ReqDisponibilidadDto reqDisponibilidadDto)
        {
            // Realiza una consulta para seleccionar los caninos de la fundación específica
            var caninos = _dbContext.Caninos.Where(c => c.IdCanino == reqDisponibilidadDto.IdCanino);

            // Actualiza la disponibilidad para los caninos seleccionados
            foreach (var canino in caninos)
            {
                canino.Disponibilidad = reqDisponibilidadDto.Disponibilidad;
            }

            // Guarda los cambios en la base de datos
            await _dbContext.SaveChangesAsync();

            return true; // Actualización exitosa de la disponibilidad
        }




    }
}
