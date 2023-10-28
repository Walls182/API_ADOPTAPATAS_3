using API_ADOPTAPATAS_3.Dtos.RequestUser;
using API_ADOPTAPATAS_3.Services;
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
        /*

        [HttpPost("/login")]
        [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> InicioSesion([FromBody] ReqLoginDto request)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest("Respuesta invalida - Invalid request");
            }

            var response = await _userService.InicioSesionAsync(request);

            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest("Inicio de sesión incorrecto");
        }
*/
    }
}
