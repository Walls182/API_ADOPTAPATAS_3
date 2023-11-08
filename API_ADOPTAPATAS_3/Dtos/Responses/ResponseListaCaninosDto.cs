namespace API_ADOPTAPATAS_3.Dtos.Responses
{
    public class ResponseListaCaninosDto
    {
        public int respuesta { get; set; }
        public int IdCanino { get; set; }

        public string? Nombre { get; set; }

        public string? Raza { get; set; }

        public int? Edad { get; set; }

        public string? Descripcion { get; set; }

        public string? Imagen { get; set; }

        public string? EstadoSalud { get; set; }

        public string? Temperamento { get; set; }

        public bool? Vacunas { get; set; }

        public bool? Disponibilidad { get; set; }

    }
}
