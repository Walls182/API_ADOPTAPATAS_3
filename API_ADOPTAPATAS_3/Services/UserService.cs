using API_ADOPTAPATAS_3.Dtos;
using API_ADOPTAPATAS_3.Repositories.Repository;
using API_ADOPTAPATAS_3.Utility;

namespace API_ADOPTAPATAS_3.Services
{
    public class UserService
    {
        private readonly JwtSettingsDto _jwtSettings;
        public UserService(JwtSettingsDto jwtSettingsDto)
        {
            _jwtSettings = jwtSettingsDto;

        }
        public ResponseLoginDto InicioSesion(RequestLoginDto requestLoginDto)
        {
            ResponseLoginDto responseLoginDto = new ResponseLoginDto();
            LoginRepository loginRepository = new LoginRepository();
            if (loginRepository.validarUsuario(requestLoginDto))
            {
                responseLoginDto = JwtUtility.GenToken(responseLoginDto, _jwtSettings);
                responseLoginDto.Respuesta = 1;
                responseLoginDto.Mensaje = "Inicio Sesion Exitoso";
            }
            else
            {
                responseLoginDto.Respuesta = 0;
                responseLoginDto.Mensaje = "Inicio Sesion Erroneo, Usuario y/o Contraseña incorrectos";
            }
            return responseLoginDto;
        }

        public ResponseGeneric CreacionUsuario(RequestRegisterDto requestRegister)
        {
            ResponseGeneric response = new ResponseGeneric();
            RegisterRepository registerRepository = new RegisterRepository();
            try
            {
                if (registerRepository.RegistrarUsuario(requestRegister))
                {
                    // Registro exitoso
                    response.respuesta = 1;
                    response.mensaje = "Creación de usuario Exitoso";
                }
                else
                {
                    // Error en el registro
                    response.respuesta = 0;
                    response.mensaje = "Error en la creación de usuario";
                }
            }
            catch (Exception ex)
            {
                // Captura la excepción y registra los detalles o propaga el error.
                Console.WriteLine("Error en CreacionUsuario: " + ex.Message);
                response.respuesta = 0;
                response.mensaje = "Error en la creación de usuario";
            }
            return response;
        }

    }
}
