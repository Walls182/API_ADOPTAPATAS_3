using API_ADOPTAPATAS_3.Dtos.RequestCanino;
using API_ADOPTAPATAS_3.Dtos.RequestFundacion;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Services;
using API_ADOPTAPATAS_3.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> RegistroUsuario([FromBody] ReqRegistroFundDto registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Solicitud de registro no válida");
            }

            try
            {
                var response = await _FundacionService.FundacionRegister(registro);

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
        [HttpPost("/buscar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseBuscarCaninoDto>> BuscarCaninoAsync(ReqBuscarCaninoDto find)
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
        public async Task<ActionResult<List<ResponseListaCaninosDto>>> ObtenerCaninosDisponiblesAsync()
        {
            try
            {
                var caninos = await _FundacionService.ObtenerCaninosDisponibles();

                if (caninos != null && caninos.Any())
                {
                    // Mapea los objetos Canino a objetos ResponseListaCaninosDto
                    var caninosDtoList = caninos.Select(canino => new ResponseListaCaninosDto
                    {
                        IdCanino = canino.IdCanino,
                        Nombre = canino.Nombre,
                        Raza = canino.Raza,
                        Edad = canino.Edad,
                        Descripcion = canino.Descripcion,
                        Imagen = canino.Imagen,
                        EstadoSalud = canino.EstadoSalud,
                        Temperamento = canino.Temperamento,
                        Vacunas = canino.Vacunas,
                        Disponibilidad = canino.Disponibilidad
                    }).ToList();

                    return Ok(caninosDtoList); // Retorna 200 OK con la lista de caninos disponibles
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found si la lista está vacía o es null
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente
                Console.WriteLine("Error al obtener la lista de caninos disponibles: " + ex.Message);
                return StatusCode(500, "Error al obtener la lista de caninos disponibles"); // Retorna 500 Internal Server Error en caso de excepción
            }
        }
        [HttpPost("/crear")]
        public async Task<ActionResult<ResponseGeneric>> CreateCaninoAsync([FromBody] ReqCrearCaninoDto reqCrearCaninoDto)
        {
            try
            {
                var registroExitoso = await _FundacionService.CreateCaninoAsync(reqCrearCaninoDto);

                if (registroExitoso != null)
                {
                    return Ok(registroExitoso.respuesta);
                }
                else
                {
                    return BadRequest(registroExitoso.respuesta);
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente
                Console.WriteLine("Error en la creación de canino: " + ex.Message);
                return StatusCode(500, new ResponseGeneric
                {
                    respuesta = 0,
                    mensaje = "Error en la creación de canino: " + ex.Message
                });
            }
        }
    }

}

