namespace API_ADOPTAPATAS_3.Dtos
{
    public class SolicitudRegistroDto
    {
        public int IdSolicitud { get; set; } // ID de la solicitud (clave primaria)

        public string NombreRepresentante { get; set; } // Nombre del representante

        public string NombreFundacion { get; set; } // Nombre de la fundación

        public string Direccion { get; set; } // Dirección de la fundación

        public string Municipio { get; set; } // Municipio de ubicación

        public string Departamento { get; set; } // Departamento de ubicación

        public string CorreoElectronico { get; set; } // Correo electrónico de contacto

        public string Telefono { get; set; } // Número de teléfono de contacto
    }
}
