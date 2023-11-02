using System;
using System.Collections.Generic;

namespace API_ADOPTAPATAS_3.Repositories.Models;

public partial class Canino
{
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

    public int? FkFundacion { get; set; }

    public int? FkEstado { get; set; }

    public virtual ICollection<Adopcion> Adopcions { get; set; } = new List<Adopcion>();

    public virtual ICollection<Donacion> Donacions { get; set; } = new List<Donacion>();

    public virtual Estado? FkEstadoNavigation { get; set; }

    public virtual Fundacion? FkFundacionNavigation { get; set; }
}
