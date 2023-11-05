using API_ADOPTAPATAS_3.Dtos.RequestFundacion;
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
                    return Ok("Creación de usuario exitosa");
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
}
