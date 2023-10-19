namespace API_ADOPTAPATAS_3.Dtos
{
    public class ResponseLoginDto
    {
        public int Respuesta { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime TiempoExpiracion { get; set; }

    }
}
