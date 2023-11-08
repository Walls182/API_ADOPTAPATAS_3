using API_ADOPTAPATAS_3.Dtos.RequestCanino;
using API_ADOPTAPATAS_3.Dtos.RequestFundacion;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Services;
using API_ADOPTAPATAS_3.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace API_ADOPTAPATAS_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundacionController : ControllerBase
    { 
       

        private readonly FundacionService _FundacionService;
        
        public FundacionController(FundacionService fundacionService)
        { 
          _FundacionService = fundacionService;
         
        }
        [HttpPost("/registroFundacion")]
        public async Task<ActionResult<ResponseGeneric>> RegistroFundacion([FromBody] ReqRegistroFundDto registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Solicitud de registro no válida");
            }

            try
            {
                var response = await _FundacionService.FundacionRegisterService(registro);

                if (response.respuesta == 1)
                {
                    return Ok(1);
                }
                else
                {
                    // Captura la excepción interna si la hay y muestra el mensaje de error.
                    return BadRequest($"Error en la creación de usuario: {response.mensaje}");
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción y muestra el mensaje de error.
                return BadRequest($"Error en la creación de usuario: {ex.Message}");
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("/buscar")]
        public async Task<ActionResult<ResponseBuscarCaninoDto>> BuscarCanino(ReqBuscarCaninoDto find)
        {
            try
            {
                var caninoResponse = await _FundacionService.FindCaninoAsync(find);

                if (caninoResponse.respuesta== 1)
                {
                    return Ok(caninoResponse); // Retorna 200 OK con la respuesta
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found si el canino no se encontró
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente
                Console.WriteLine("Error en la búsqueda: " + ex.Message);
                return StatusCode(500, "Error en la búsqueda"); // Retorna 500 Internal Server Error en caso de excepción
            }
        }
        [HttpPost("/disponibles")]
        public async Task<ActionResult<List<ResponseListaCaninosDto>>> ObtenerCaninosDisponibles()
        {
            try
            {
                var caninosResponse = await _FundacionService.ObtenerCaninosDisponibles();

                if (caninosResponse.Count > 0)
                {
                    return Ok(caninosResponse); // Retorna 200 OK con la lista de caninos disponibles
                }
                else
                {
                    return NotFound("No se encontraron caninos disponibles"); // Retorna 404 Not Found si no se encontraron caninos disponibles
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente
                Console.WriteLine("Error en la obtención de caninos disponibles: " + ex.Message);
                return StatusCode(500, "Error en la obtención de caninos disponibles"); // Retorna 500 Internal Server Error en caso de excepción
            }
        }
            [HttpPost("/crear")]
        public async Task<ActionResult<ResponseGeneric>> CreateCanino([FromBody] ReqCrearCaninoDto reqCrearCaninoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Solicitud de registro no válida");
            }

            try
            {
                var response = await _FundacionService.CreateCaninoService(reqCrearCaninoDto);

                if (response.respuesta == 1)
                {
                    return Ok(response);
                }
                else
                {
                    // Captura la excepción interna si la hay y muestra el mensaje de error.
                    return BadRequest($"Error en la creación de usuario: {response.mensaje}");
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción y muestra el mensaje de error.
                return BadRequest($"Error en la creación de usuario: {ex.Message}");
            }
        }

      
        
    }

}//

