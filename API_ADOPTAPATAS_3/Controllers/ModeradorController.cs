using API_ADOPTAPATAS_3.Dtos.RequestFundacion;
using API_ADOPTAPATAS_3.Dtos.RequestModerador;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace API_ADOPTAPATAS_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeradorController : ControllerBase
    {
        private readonly ModeradorService _moderadorService;
        public ModeradorController(ModeradorService moderador)
        {
            _moderadorService = moderador;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("/cambiorol")]
        public async Task<ActionResult<ResponseGeneric>> CambioRol([FromBody] ReqCambioRolDto rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Solicitud de registro no válida");
            }

            try
            {
                var response = await _moderadorService.CambioRolService(rol);

                if (response.respuesta == 1)
                {
                    return Ok(response);
                }
                else
                {
                    // Captura la excepción interna si la hay y muestra el mensaje de error.
                    return BadRequest($"Error en el cambio de rol: {response.mensaje}");
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción y muestra el mensaje de error.
                return BadRequest($"Error en el cambio de rol: {ex.Message}");
            }
        }
        /*
        [HttpPost("/cambioestado")]
        public async Task<ActionResult<ResponseGeneric>> CambioEstado([FromBody] ReqCambioEstadoDto estado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Solicitud de registro no válida");
            }

            try
            {
                var response = await _moderadorService.CambioEstadoService(estado);

                if (response.respuesta == 1)
                {
                    return Ok(1);
                }
                else
                {
                    // Captura la excepción interna si la hay y muestra el mensaje de error.
                    return BadRequest($"Error en el cambio del estado: {response.mensaje}");
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción y muestra el mensaje de error.
                return BadRequest($"Error en el cambio del estado: {ex.Message}");
            }
        }
        */
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("/activarfundacion")]
        public async Task<ActionResult<ResponseGeneric>> ActivarFundacionService([FromBody] ReqActualizarFundacionDto fundacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Solicitud de registro no válida");
            }

            try
            {
                var response = await _moderadorService.ActivarFundacionService(fundacion);

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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("/obtenerfundaciones")]
        public async Task<ActionResult<List<ResponseListaFundacionesDto>>> ObtenerFundaciones()
        {

            try
            {
                var fundacionResponse = await _moderadorService.ObtenerFundaciones();

                if (fundacionResponse.Count > 0)
                {
                    return Ok(fundacionResponse); // Retorna 200 OK con la lista de caninos disponibles
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
    }
}
