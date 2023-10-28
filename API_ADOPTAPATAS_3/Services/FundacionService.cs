using API_ADOPTAPATAS_3.Dtos.RequestCanino;
using API_ADOPTAPATAS_3.Dtos.RequestUser;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Repositories.Repository;

namespace API_ADOPTAPATAS_3.Services
{
    public class FundacionService
    {
        private readonly FundacionRepository _FundacionRepository;
        FundacionService(FundacionRepository fundacionRepository)
        {
            _FundacionRepository = fundacionRepository;
        }
        public async Task<ResponseBuscarCanino> BuscarCaninoAsync(ReqBuscarCaninoDto find)
        {
            try
            {
                var Canino = await _FundacionRepository.BuscarCanino(find);

                if (Canino != null)
                {
                    return new ResponseBuscarCanino
                    {
                        respuesta = 1,
                        mensaje = "Búsqueda exitosa",
                        IdCanino = Canino.IdCanino,
                        Nombre = Canino.Nombre,
                        Raza = Canino.Raza,
                        Edad = Canino.Edad,
                        Descripcion = Canino.Descripcion,
                        Imagen = Canino.Imagen,
                        EstadoSalud = Canino.EstadoSalud,
                        Temperamento = Canino.Temperamento,
                        Vacunas = Canino.Vacunas,
                        Disponibilidad = Canino.Disponibilidad
                    };
                }
                else
                {
                    return new ResponseBuscarCanino
                    {
                        respuesta = 0,
                        mensaje = "Canino no encontrado"
                    };
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error en la búsqueda: " + ex.Message);
                // Puedes lanzar una excepción si deseas propagar el error
                throw;
            }
        }
        public async Task<ResponseGeneric> CreacionCaninoAsync(ReqCrearCaninoDto reqCrearCaninoDto)
        {
            try
            {
                var registroExitoso = await _FundacionRepository.CrearCaninoAsync(reqCrearCaninoDto);

                return new ResponseGeneric
                {
                    respuesta = registroExitoso ? 1 : 0,
                    mensaje = registroExitoso ? "Creación de usuario exitosa" : "Error en la creación de usuario"
                };
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error en CreacionUsuario: " + ex.Message);

                // Devolver una respuesta detallada sobre el error
                return new ResponseGeneric
                {
                    respuesta = 0,
                    mensaje = "Error en la creación de usuario: " + ex.Message
                };
            }
        }
        public async Task<ResponseGeneric> ActualizarCaninoAsync(ReqActualizarCaninoDto respActualizarCaninoDto)
        {
            try
            {
                var registroExitoso = await _FundacionRepository.ActualizarCaninoAsync(respActualizarCaninoDto);

                return new ResponseGeneric
                {
                    respuesta = registroExitoso ? 1 : 0,
                    mensaje = registroExitoso ? "Actualizacion de usuario exitosa" : "Error en la actualizacion de usuario"
                };
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error en Actualizacion Usuario: " + ex.Message);

                // Devolver una respuesta detallada sobre el error
                return new ResponseGeneric
                {
                    respuesta = 0,
                    mensaje = "Error en la actualizacion de usuario: " + ex.Message
                };
            }






        }
       /*
        * 
        * Preguntar como puedo devolver una lista de todos los caninos en la bd
        * 
        * public async Task<ResponseListaCaninosDto> ListarCaninos()
        {
         
        }*/
    }
}
