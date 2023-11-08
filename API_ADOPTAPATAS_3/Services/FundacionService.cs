using API_ADOPTAPATAS_3.Dtos.RequestCanino;
using API_ADOPTAPATAS_3.Dtos.RequestFundacion;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Repositories.Repository;
using API_ADOPTAPATAS_3.Utility;
using System.Net;
using System.Net.Mail;

namespace API_ADOPTAPATAS_3.Services
{
    public class FundacionService
    {
        private readonly FundacionRepository _FundacionRepository;
       
        public FundacionService(FundacionRepository fundacionRepository)
        {
            _FundacionRepository = fundacionRepository;
        }
        public async Task<ResponseGeneric> FundacionRegisterService(ReqRegistroFundDto requestRegister)
        {
            try
            {
                var registroExitoso = await _FundacionRepository.RegistrarFundacion(requestRegister);
              
                string email = "tukodatabases@gmail.com";
                string password = "fulscagiehwazjnp";
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;


                var client = new SmtpClient(smtpServer, smtpPort)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(email, password),
                    EnableSsl = true,
                };

                var message = new MailMessage
                    (email,
                    requestRegister.Correo,
                    "RESPUESTA SOLICITUD REGISTRO",
                    "bienvenido "+requestRegister.NombreFundacion+" " +
                    "");

                // Crea el cuerpo del mensaje en formato HTML
                string body = @"
                                <h1>Bienvenido a Adoptapatas Conect</h1>
                                <p>Gracias por querer hacer parte de nuestra comunidad. Estamos emocionados de tenerte como parte de Adoptapatas.</p>
                                   <p>En las proximas semanas recibiras la confirmacion de tu registro,nuestros asesores validaran los datos. En caso positivo te enviaremos las credenciales de ingreso a ADOPTAPATAS CONECT </p>
                                   <img src='https://i.pinimg.com/736x/83/c3/7c/83c37c101f433d7c2eea87a18e3f45b5.jpg' alt='Imagen de bienvenida'>
                                " + "<h2>TE DAMOS LAS GRACIAS DE PARTE DE NUESTRO EQUIPO DE TRABAJO</h1>   ";
                message.IsBodyHtml = true;
                message.Body = body;

                client.Send(message);
                return new ResponseGeneric
                {
                    respuesta = registroExitoso ? 1 : 0,
                    mensaje = registroExitoso ? "Creación de usuario exitosa" : "Error en la creación de usuario, usuario ya existe"
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
                    mensaje = "Error en la creación de usuario: " + ex.Message
                };
            }
        }
        public async Task<ResponseBuscarCaninoDto> FindCaninoAsync(ReqBuscarCaninoDto find)
        {
            try
            {
                var Canino = await _FundacionRepository.BuscarCanino(find);

                if (Canino != null )
                {
                    return new ResponseBuscarCaninoDto
                    {
                        respuesta = 1,
                        IdCanino = Canino.IdCanino,
                        Nombre = Canino.Nombre,
                        Raza = Canino.Raza,
                        Edad = Canino.Edad,
                        Descripcion = Canino.Descripcion,
                        Imagen = Canino.Imagen,
                        EstadoSalud = Canino.EstadoSalud,
                        Temperamento = Canino.Temperamento,
                        Vacunas = Canino.Vacunas,
                        Disponibilidad = Canino.Disponibilidad
                    };
                }
                else
                {
                    return new ResponseBuscarCaninoDto
                    {
                        respuesta = 0
                    };
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error en la búsqueda: " + ex.Message);
                // Puedes lanzar una excepción si deseas propagar el error
                throw;
            }
        }
        public async Task<List<ResponseListaCaninosDto>> ObtenerCaninosDisponibles()
        {
            // Utiliza el método del repositorio para obtener la lista de caninos disponibles
            var caninos = await _FundacionRepository.ObtenerCaninosDisponibles();

            if (caninos != null && caninos.Any())
            {
                var caninosDtoList = new List<ResponseListaCaninosDto>();

                foreach (var canino in caninos)
                {
                    var caninoDto = new ResponseListaCaninosDto
                    {
                        IdCanino = canino.IdCanino,
                        respuesta = 1,
                        Nombre = canino.Nombre,
                        Raza = canino.Raza,
                        Edad = canino.Edad,
                        Descripcion = canino.Descripcion,
                        Imagen = canino.Imagen,
                        EstadoSalud = canino.EstadoSalud,
                        Temperamento = canino.Temperamento,
                        Vacunas = canino.Vacunas,
                        Disponibilidad = canino.Disponibilidad
                    };

                    caninosDtoList.Add(caninoDto);
                }

                return caninosDtoList;
            }
            else
            {
                // Si la lista está vacía o es null, puedes devolver una lista vacía o un mensaje apropiado
                return new List<ResponseListaCaninosDto>();
            }
        }
        public async Task<ResponseGeneric> CreateCaninoService(ReqCrearCaninoDto reqCrearCaninoDto)
        {
            try
            {
                var registroExitoso = await _FundacionRepository.CrearCanino(reqCrearCaninoDto);

                return new ResponseGeneric
                {
                    respuesta = registroExitoso ? 1 : 0,
                    mensaje = registroExitoso ? "Creación de usuario exitosa" : "Error en la creación de usuario"
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
                    mensaje = "Error en la creación de usuario: " + ex.Message
                };
            }
        }

       





        //-------------------------A estos no los llama el front :( pero estan hechos-------------------






        public async Task<ResponseGeneric> UpdateCaninoAsync(ReqActualizarCaninoDto respActualizarCaninoDto)
        {
            try
            {
                var registroExitoso = await _FundacionRepository.ActualizarCaninoAsync(respActualizarCaninoDto);

                return new ResponseGeneric
                {
                    respuesta = registroExitoso ? 1 : 0,
                    mensaje = registroExitoso ? "Actualizacion de usuario exitosa" : "Error en la actualizacion de usuario"
                };
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error en Actualizacion Usuario: " + ex.Message);

                // Devolver una respuesta detallada sobre el error
                return new ResponseGeneric
                {
                    respuesta = 0,
                    mensaje = "Error en la actualizacion de usuario: " + ex.Message
                };
            }






        }
        public async Task<ResponseGeneric> UpdateDispoAsync (ReqDisponibilidadDto reqDisponibilidadDto)
        {
            try
            {
                var registroExitoso = await _FundacionRepository.ActualizarDisponibilidad(reqDisponibilidadDto);

                return new ResponseGeneric
                {
                    respuesta = registroExitoso ? 1 : 0,
                    mensaje = registroExitoso ? "Actualizacion de usuario exitosa" : "Error en la actualizacion de usuario"
                };
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente (por ejemplo, registrarla o propagarla)
                Console.WriteLine("Error en Actualizacion Usuario: " + ex.Message);

                // Devolver una respuesta detallada sobre el error
                return new ResponseGeneric
                {
                    respuesta = 0,
                    mensaje = "Error en la actualizacion de usuario: " + ex.Message
                };
            }
        }
        public async Task<List<ResponseListaCaninosDto>> ObtenerCaninosPorFundacion(ReqIdFunDto idFundacion)
        {
            // Utiliza el método del repositorio para obtener la lista de caninos asociados a una fundación específica
            var caninos = await _FundacionRepository.ObtenerCaninosPorFundacionAsync(idFundacion);

            if (caninos != null && caninos.Any())
            {
                var caninosDtoList = new List<ResponseListaCaninosDto>();

                foreach (var canino in caninos)
                {
                    var caninoDto = new ResponseListaCaninosDto
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
                    };

                    caninosDtoList.Add(caninoDto);
                }

                return caninosDtoList;
            }
            else
            {
                // Si la lista está vacía o es null, puedes devolver una lista vacía o un mensaje apropiado
                return new List<ResponseListaCaninosDto>();
            }
        }




    }
}
