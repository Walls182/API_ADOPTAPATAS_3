﻿namespace API_ADOPTAPATAS_3.Dtos.Responses
{
    public class ResponseLoginDto
    {
        public int Respuesta { get; set; }
        public int? IdRol { get; set; }
        public int? IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime TiempoExpiracion { get; set; }

     

    }
}
