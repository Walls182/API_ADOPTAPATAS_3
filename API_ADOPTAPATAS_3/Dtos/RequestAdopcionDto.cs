namespace API_ADOPTAPATAS_3.Dtos
{
    public class RequestAdopcionDto
    {
        public DateTime? FechaAdopcion { get; set; }
        public string? NombreCanino { get; set; }
        public int? EdadCanino { get; set; }

        public string? RazaCanino { get; set; }
        public string? NombreUsuario { get; set; }

        public string? CorreoUsuario { get; set; }
    }
}
