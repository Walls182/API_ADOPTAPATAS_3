using API_ADOPTAPATAS_3.Dtos.DtoUser;
using API_ADOPTAPATAS_3.Dtos.RequestUser;
using API_ADOPTAPATAS_3.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace API_ADOPTAPATAS_3.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> InicioSesion([FromBody] ReqLoginDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Solicitud no válida");
            }

            var response = await _userService.InicioSesionAsync(request);

            if (response.Respuesta == 1)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("registro")]
        public async Task<IActionResult> RegistroUsuario([FromBody] ReqRegisterDto registroDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Solicitud no válida");
            }

            var response = await _userService.CreacionUsuarioAsync(registroDto);

            if (response.respuesta == 1)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("donaciones")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RealizarDonacion([FromBody] ReqDonacionDto donacionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Solicitud no válida");
            }

            var response = await _userService.CreacionDonacionAsync(donacionDto);

            if (response.respuesta == 1)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("adopciones")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RealizarAdopcion([FromBody] ReqAdopcionDto adopcionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Solicitud no válida");
            }

            var response = await _userService.CreacionAdopcionAsync(adopcionDto);

            if (response.respuesta == 1)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}

