
using API_ADOPTAPATAS_3.Dtos.RequestModerador;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Repositories.Repository;

namespace API_ADOPTAPATAS_3.Services
{
    public class ModeradorService
    {
      private readonly ModeradorRepository _repository;

        public ModeradorService(ModeradorRepository repository) 
        {
        _repository = repository;

        }
        public async Task<ResponseGeneric> CambioRol(ReqCambioRolDto cambio)
        {
            try
            {
                var registroExitoso = await _repository.CambiarRolUsuarioAsync(cambio);

                return new ResponseGeneric
                {
                    respuesta = registroExitoso ? 1 : 0,
                };
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error en Cambio de ROl: " + ex.Message);

                // Devolver una respuesta detallada sobre el error
                return new ResponseGeneric
                {
                    respuesta = 0,
                    mensaje = "Error en el Cambio de Rol: " + ex.Message
                };
            }
        }

        public async Task<ResponseGeneric> CambioEstado(ReqCambioRolDto cambio)
        {
            try
            {
                var registroExitoso = await _repository.CambiarRolUsuarioAsync(cambio);

                return new ResponseGeneric
                {
                    respuesta = registroExitoso ? 1 : 0,
                
                };
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error en Cambio de estado: " + ex.Message);

                // Devolver una respuesta detallada sobre el error
                return new ResponseGeneric
                {
                    respuesta = 0,
                    mensaje = "Error en el Cambio de estado: " + ex.Message
                };
            }
        }
        public async Task<ResponseGeneric> ActivarFundacion(ReqActualizarFundacionDto idfundacion)
        {
            try
            {
                var registroExitoso = await _repository.ActivarFundacionAsync(idfundacion);

                return new ResponseGeneric
                {
                    respuesta = registroExitoso ? 1 : 0,
                  
                };
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                

                // Devolver una respuesta detallada sobre el error
                return new ResponseGeneric
                {
                    respuesta = 0,
                   
                };
            }
        }
    }
}
