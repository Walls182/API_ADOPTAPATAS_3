using API_ADOPTAPATAS_3.Dtos.DtoUser;
using API_ADOPTAPATAS_3.Dtos.RequestUser;
using API_ADOPTAPATAS_3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("/login")]
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

        [HttpPost("/registro")]
        public async Task<IActionResult> RegistroUsuario([FromBody] ReqRegisterDto registroDto)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest("Solicitud de registro no válida");
            }

            var response = await _userService.CreacionUsuarioAsync(registroDto);

            if (response.respuesta == 1)
            {
                return Ok("Creación de usuario exitosa");
            }

            return BadRequest("Error en la creación de usuario");
        }

        [HttpPost("/donaciones")]
        public async Task<IActionResult> RealizarDonacion([FromBody] ReqDonacionDto donacionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Solicitud de registro no válida");
            }

            var response = await _userService.CreacionDonacionAsync(donacionDto);

            if (response.respuesta == 1)
            {
                return Ok("Creación de usuario exitosa");
            }

            return BadRequest("Error en la creación de usuario");
        }

        [HttpPost("/adopciones")]
        public async Task<IActionResult> RealizarAdopcion([FromBody] ReqAdopcionDto adopcionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Solicitud de registro no válida");
            }

            var response = await _userService.CreacionAdopcionAsync(adopcionDto);

            if (response.respuesta == 1)
            {
                return Ok("Creación de usuario exitosa");
            }

            return BadRequest("Error en la creación de usuario");
        }




    }
}
