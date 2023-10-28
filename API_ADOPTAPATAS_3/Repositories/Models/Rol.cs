using System;
using System.Collections.Generic;

namespace API_ADOPTAPATAS_3.Repositories.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Fundacion> Fundacions { get; set; } = new List<Fundacion>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
