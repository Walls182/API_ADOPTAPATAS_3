using API_ADOPTAPATAS_3.Dtos.RequestCanino;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Repositories.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_ADOPTAPATAS_3.Repositories.Repository
{
    public class FundacionRepository
    {
        BdadoptapatasContext _dbContext = new BdadoptapatasContext();

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
