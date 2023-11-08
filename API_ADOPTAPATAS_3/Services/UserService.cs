using API_ADOPTAPATAS_3.Dtos.DtoUser;
using API_ADOPTAPATAS_3.Dtos.RequestUser;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Repositories.Repository;
using API_ADOPTAPATAS_3.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace API_ADOPTAPATAS_3.Services
{
    public class UserService
    {
        private readonly JwtSettingsDto _jwtSettings;
        private readonly UserRepository _userRepository;

        public UserService(JwtSettingsDto jwtSettingsDto, UserRepository userRepository)
        {
            _jwtSettings = jwtSettingsDto;
            _userRepository = userRepository;
        }

        public async Task<ResponseLoginDto> InicioSesionAsync(ReqLoginDto requestLoginDto)
        {
            var responseLoginDto = new ResponseLoginDto();

            var idRol = await _userRepository.ObtenerIdRolUsuarioAsync(requestLoginDto);

            if (idRol.HasValue)
            {
                responseLoginDto = JwtUtility.GenToken(responseLoginDto, _jwtSettings);
                responseLoginDto.Respuesta = 1;
                responseLoginDto.IdRol = idRol.Value;
            }
            else
            {
                responseLoginDto.Respuesta = 0;
            }

            return responseLoginDto;
        }

        public async Task<ResponseGeneric> CreacionUsuarioAsync(ReqRegisterDto requestRegister)
        {
            try
            {
                var registroExitoso = await _userRepository.RegistrarUsuarioAsync(requestRegister);

                return new ResponseGeneric
                {
                    respuesta = registroExitoso ? 1 : 0
                };
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error en CreacionUsuario: " + ex.Message);

                return new ResponseGeneric
                {
                    respuesta = 0
                };
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ResponseGeneric> CreacionDonacionAsync(ReqDonacionDto donacionDto)
        {
            var responseGeneric = new ResponseGeneric();

            try
            {
                if (await _userRepository.RealizarDonacionAsync(donacionDto))
                {
                    responseGeneric.respuesta = 1;
                }
                else
                {
                    responseGeneric.respuesta = 0;
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error en CreacionDonacionAsync: " + ex.Message);

                responseGeneric.respuesta = 0;
                responseGeneric.mensaje = "Error en la: " + ex.Message;
            }

            return responseGeneric;
        }

        public async Task<ResponseGeneric> CreacionAdopcionAsync(ReqAdopcionDto adopcionDto)
        {
            var responseGeneric = new ResponseGeneric();

            try
            {
                if (await _userRepository.RealizarAdopcionAsync(adopcionDto))
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
                Console.WriteLine("Error en CreacionAdopcionAsync: " + ex.Message);

                responseGeneric.respuesta = 0;
                responseGeneric.mensaje = "Error en: " + ex.Message;
            }

            return responseGeneric;
        }
    }


}
