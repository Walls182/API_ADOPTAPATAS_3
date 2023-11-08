using API_ADOPTAPATAS_3.Dtos.RequestCanino;
using API_ADOPTAPATAS_3.Dtos.RequestFundacion;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Repositories.Models;
using API_ADOPTAPATAS_3.Utility;
using Microsoft.EntityFrameworkCore;

namespace API_ADOPTAPATAS_3.Repositories.Repository
{
    public class FundacionRepository
    {
        private readonly BdadoptapatasContext _dbContext;
        public FundacionRepository(BdadoptapatasContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> RegistrarFundacion(ReqRegistroFundDto fundacionDto)
        {

            // Verifica si la fundación ya está registrada por su nombre de fundación y correo
            var fundacionExistente = await _dbContext.Fundacions
                .AnyAsync(f => f.NombreFundacion == fundacionDto.NombreFundacion && f.Correo == fundacionDto.Correo);

            if (fundacionExistente)
            {
                return false; // Fundación ya registrada
            }

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
                FkRol = 2,
                FkEstado = 2,
                FkLogin = 8
            };

            _dbContext.Fundacions.Add(nuevaFundacion);


            await _dbContext.SaveChangesAsync();




            return true; // Registro exitoso

        }

        public async Task<Canino> BuscarCanino(ReqBuscarCaninoDto find)
        {
            // Realiza la búsqueda en la base de datos
            var canino = await _dbContext.Caninos.FirstOrDefaultAsync(u =>
                u.Nombre == find.Nombre ||
                u.Raza == find.Raza ||
                u.Edad == find.Edad);

            if (canino == null)
            {
                return null;// Devuelve una respuesta indicando que no se encontró ningún canino

            }

            // Devuelve el canino encontrado
            return canino;
        }
        public async Task<List<Canino>> ObtenerCaninosDisponibles()
        {
            // Realiza una consulta para obtener todos los caninos con disponibilidad en true
            var caninosDisponibles = await _dbContext.Caninos
                .Where(c => c.Disponibilidad == true)
                .ToListAsync();

            return caninosDisponibles;
        }
        public async Task<bool> CrearCanino(ReqCrearCaninoDto canino)
        {

            // Verifica si el canino ya esta registrado
            bool fundacionExistente = await _dbContext.Caninos
                .AnyAsync(f => f.Nombre == canino.Nombre && f.Raza == canino.Raza);

            if (fundacionExistente)
            {
                return false;
            }

            // Crea un objeto Fundacion con los datos proporcionados
            var nuevoCanino = new Canino
            {
                Nombre = canino.Nombre,
                Raza = canino.Raza,
                Edad = canino.Edad,
                Descripcion = canino.Descripcion,
                Imagen = canino.Imagen,
                EstadoSalud = canino.EstadoSalud,
                Temperamento = canino.Temperamento,
                Vacunas = canino.Vacunas,
                Disponibilidad = canino.Disponibilidad,
                FkFundacion = canino.FkFundacion,
                FkEstado = 1

            };

            _dbContext.Caninos.Add(nuevoCanino);


            await _dbContext.SaveChangesAsync();




            return true; // Registro exitoso
        }
    
      

        //----------------------------------------------no------------------------

        public async Task<Fundacion> ObtenerFundacionAsync(ReqIdFunDto idFundacion)
        {
            // Realiza una consulta para obtener una fundación específica por su ID
            var fundacion = await _dbContext.Fundacions
                .FirstOrDefaultAsync(f => f.IdFundacion == idFundacion.IdFundacion);

            return fundacion;
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
