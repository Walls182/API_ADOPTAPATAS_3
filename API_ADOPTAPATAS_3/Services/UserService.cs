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
        private readonly UserRepository _UserRepository;
        
    
        

        public UserService(JwtSettingsDto jwtSettingsDto, UserRepository userRepository)
        {
            _jwtSettings = jwtSettingsDto;
            _UserRepository = userRepository;
        }

        public async Task<ResponseLoginDto> InicioSesionAsync(ReqLoginDto requestLoginDto)
        {
            ResponseLoginDto responseLoginDto = new ResponseLoginDto();

            var idRol = await _UserRepository.ObtenerIdRolUsuarioAsync(requestLoginDto);

            if (idRol.HasValue) 
            {
                responseLoginDto = JwtUtility.GenToken(responseLoginDto, _jwtSettings);
                responseLoginDto.Respuesta = 1;
                responseLoginDto.IdRol = idRol.Value; // Asignamos el IdRol
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
                var registroExitoso = await _UserRepository.RegistrarUsuarioAsync(requestRegister);

                return new ResponseGeneric
                {
                    respuesta = registroExitoso ? 1 : 0,
  
                };
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error en CreacionUsuario: " + ex.Message);

                // Devolver una respuesta detallada sobre el error
                return new ResponseGeneric
                {
                    respuesta = 0,
            
                };
            }
        }

        //-------------------------------Dejada por el momento en usuarios
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ResponseGeneric> CreacionDonacionAsync(ReqDonacionDto donacionDto)
        {
            ResponseGeneric responseGeneric = new ResponseGeneric();

            try
            {
                if (await _UserRepository.RealizarDonacionAsync(donacionDto))
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
                Console.WriteLine("Error: " + ex.Message);

                // Devolver una respuesta detallada sobre el error
                responseGeneric.respuesta = 0;
                responseGeneric.mensaje = "Error en la: " + ex.Message;
            }

            return responseGeneric;
        }
        //-------------------------------Dejada por el momento en usuarios
      
        public async Task<ResponseGeneric> CreacionAdopcionAsync(ReqAdopcionDto adopcionDto)
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
