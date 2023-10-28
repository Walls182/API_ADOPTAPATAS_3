using System;
using System.Collections.Generic;

namespace API_ADOPTAPATAS_3.Repositories.Models;

public partial class Login
{
    public int IdLogin { get; set; }

    public string? Usuario { get; set; }

    public string? Contrasena { get; set; }

    public virtual ICollection<Fundacion> Fundacions { get; set; } = new List<Fundacion>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
