using API_ADOPTAPATAS_3.Dtos;
using API_ADOPTAPATAS_3.Dtos.DtoUser;
using API_ADOPTAPATAS_3.Dtos.RequestUser;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Repositories.Models;
using API_ADOPTAPATAS_3.Repositories.Repository;
using API_ADOPTAPATAS_3.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol.Core.Types;
using System.Net.Mail;
using System.Net;

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
            var responseLoginDto = await _userRepository.ObtenerRolIdUsuarioAsync(requestLoginDto);

            if (responseLoginDto.Respuesta == 1)
            {
                responseLoginDto = JwtUtility.GenToken(responseLoginDto, _jwtSettings);
                
            }

            return responseLoginDto;
        }


        public async Task<ResponseGeneric> CreacionUsuarioAsync(ReqRegisterDto requestRegister)
        {
            try
            {
                var registroExitoso = await _userRepository.RegistrarUsuarioAsync(requestRegister);
                string email = "tukodatabases@gmail.com";
                string password = "fulscagiehwazjnp";
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;

                if (registroExitoso== true)
                {

                    var client = new SmtpClient(smtpServer, smtpPort)
                    {
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(email, password),
                        EnableSsl = true,
                    };

                    var message = new MailMessage
                        (email, requestRegister.Correo,
                        "CONFIRMACION DE REGISTRO",
                        "BIENVENIDA A " + requestRegister.Nombre +
                        "Tu solicitud de registro fue aprobada ," +
                        " disfruta tu experiencia" +
                        "");
                    // Crea el cuerpo del mensaje en formato HTML
                    string body = @"
                                    <h1>Bienvenido a Adoptapatas Conect</h1>
                                    <p>" + requestRegister.Nombre + @" Gracias por unirte a nuestra comunidad. Estamos emocionados de tenerte como parte de ADOPTAPATASCONNECT.</p>
                                    <img src='https://th.bing.com/th/id/OIP.tdtoOX9iX1gqLR4Kvf7_3gHaHa?pid=ImgDet&rs=1' alt='Imagen de bienvenida' />
                                    </p>
                                    <h2>TE DAMOS LAS GRACIAS EN NUESTRO EQUIPO DE TRABAJO</h2>
                                ";

                    message.IsBodyHtml = true;
                    message.Body = body;

                    client.Send(message);
                }
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
