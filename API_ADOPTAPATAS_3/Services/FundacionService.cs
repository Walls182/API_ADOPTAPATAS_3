using API_ADOPTAPATAS_3.Dtos.RequestCanino;
using API_ADOPTAPATAS_3.Dtos.RequestFundacion;
using API_ADOPTAPATAS_3.Dtos.Responses;
using API_ADOPTAPATAS_3.Repositories.Repository;
using API_ADOPTAPATAS_3.Utility;

namespace API_ADOPTAPATAS_3.Services
{
    public class FundacionService
    {
        private readonly FundacionRepository _FundacionRepository;
        private readonly IMailSender _MailSender;
        public FundacionService(FundacionRepository fundacionRepository, IMailSender mailSender)
        {
            _FundacionRepository = fundacionRepository;
            _MailSender = mailSender;
        }


        public async Task<ResponseGeneric> FundacionRegister(ReqRegistroFundDto requestRegister)
        {
            try
            {
                var registroExitoso = await _FundacionRepository.RegistrarFundacionAsync(requestRegister);

                //Envía un mensaje de confirmación por correo
                string mensajeCorreo = "<html><body>";
                mensajeCorreo += "<h2>Confirmación de solicitud recibida</h2>";
                mensajeCorreo += "<p>¡Tu solicitud ha sido recibida con éxito! Pronto recibirás una respuesta con las credenciales para iniciar sesión en la plataforma.</p>";
                mensajeCorreo += "</body></html>";
                await _MailSender.SendEmailHtmlAsync(requestRegister.Correo, "Confirmación de registro", mensajeCorreo);

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

                if (Canino != null)
                {
                    return new ResponseBuscarCaninoDto
                    {
                        respuesta = 1,
                        mensaje = "Búsqueda exitosa",
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
                        respuesta = 0,
                        mensaje = "Canino no encontrado"
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
        public async Task<ResponseGeneric> CreateCaninoAsync(ReqCrearCaninoDto reqCrearCaninoDto)
        {
            try
            {
                var registroExitoso = await _FundacionRepository.CrearCaninoAsync(reqCrearCaninoDto);

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

        public async Task<List<ResponseListaCaninosDto>> ObtenerCaninosDisponibles()
        {
            // Utiliza el método del repositorio para obtener la lista de caninos disponibles
            var caninos = await _FundacionRepository.ObtenerCaninosDisponiblesAsync();

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
