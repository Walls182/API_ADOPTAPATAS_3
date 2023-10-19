using System;
using System.Collections.Generic;

namespace API_ADOPTAPATAS_3.Repositories.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Correo { get; set; }

    public string? Celular { get; set; }

    public string? Direccion { get; set; }

    public string? Municipio { get; set; }

    public string? Departamento { get; set; }

    public int? FkLogin { get; set; }

    public int? FkRol { get; set; }

    public int? FkEstado { get; set; }

    public virtual ICollection<Adopcion> Adopcions { get; set; } = new List<Adopcion>();

    public virtual ICollection<Donacion> Donacions { get; set; } = new List<Donacion>();

    public virtual Estado? FkEstadoNavigation { get; set; }

    public virtual Login? FkLoginNavigation { get; set; }

    public virtual Rol? FkRolNavigation { get; set; }
}
