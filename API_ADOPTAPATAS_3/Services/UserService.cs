using API_ADOPTAPATAS_3.Dtos;
using API_ADOPTAPATAS_3.Repositories.Repository;
using API_ADOPTAPATAS_3.Utility;

namespace API_ADOPTAPATAS_3.Services
{
    public class UserService
    {
        private readonly JwtSettingsDto _jwtSettings;
        private readonly UserRepository _UserRepository;
        
    
        

        public UserService(JwtSettingsDto jwtSettingsDto, UserRepository userRepository)
        {
            _jwtSettings = jwtSettingsDto;
            _UserRepository = userRepository;
        }

        public async Task<ResponseLoginDto> InicioSesionAsync(RequestLoginDto requestLoginDto)
        {
            ResponseLoginDto responseLoginDto = new ResponseLoginDto();

            if (await _UserRepository.ValidarUsuarioAsync(requestLoginDto))
            {
               
               
                responseLoginDto = JwtUtility.GenToken(responseLoginDto, _jwtSettings);
                responseLoginDto.Respuesta = 1;
                responseLoginDto.Mensaje = "Inicio Sesión Exitoso";
            }
            else
            {
                responseLoginDto.Respuesta = 0;
                responseLoginDto.Mensaje = "Inicio Sesión Erroneo, Usuario y/o Contraseña incorrectos";
            }

            return responseLoginDto;
        }

        public async Task<ResponseGeneric> CreacionUsuarioAsync(RequestRegisterDto requestRegister)
        {
            ResponseGeneric responseGeneric = new ResponseGeneric();

            try
            {
                if (await _UserRepository.RegistrarUsuarioAsync(requestRegister))
                {
                    responseGeneric.respuesta = 1;
                    responseGeneric.mensaje = "Creación de usuario Exitoso";
                }
                else
                {
                    responseGeneric.respuesta = 0;
                    responseGeneric.mensaje = "Error en la creación de usuario";
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error en CreacionUsuario: " + ex.Message);

                // Devolver una respuesta detallada sobre el error
                responseGeneric.respuesta = 0;
                responseGeneric.mensaje = "Error en la creación de usuario: " + ex.Message;
            }

            return responseGeneric;
        }

        public async Task<ResponseGeneric> CreacionDonacionAsync(RequestDonacionDto donacionDto)
        {
            ResponseGeneric responseGeneric = new ResponseGeneric();

            try
            {
                if (await _UserRepository.RealizarDonacionAsync(donacionDto))
                {
                    responseGeneric.respuesta = 1;
                    responseGeneric.mensaje = "Creación de Donacion Exitoso";
                }
                else
                {
                    responseGeneric.respuesta = 0;
                    responseGeneric.mensaje = "Error en la creación de la donacion";
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error: " + ex.Message);

                // Devolver una respuesta detallada sobre el error
                responseGeneric.respuesta = 0;
                responseGeneric.mensaje = "Error en la: " + ex.Message;
            }

            return responseGeneric;
        }

        public async Task<ResponseGeneric> CreacionAdopcionAsync(RequestAdopcionDto adopcionDto)
        {
            ResponseGeneric responseGeneric = new ResponseGeneric();

            try
            {
                if (await _UserRepository.RealizarAdopcionAsync(adopcionDto))
                {
                    responseGeneric.respuesta = 1;
                    responseGeneric.mensaje = "Creación de Adopcion exitoso";
                }
                else
                {
                    responseGeneric.respuesta = 0;
                    responseGeneric.mensaje = "Error en la creación de la adopcion";
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error en: " + ex.Message);

                // Devolver una respuesta detallada sobre el error
                responseGeneric.respuesta = 0;
                responseGeneric.mensaje = "Error en: " + ex.Message;
            }

            return responseGeneric;
        }


    }

}
