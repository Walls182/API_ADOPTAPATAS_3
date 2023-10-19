using System;
using System.Collections.Generic;

namespace API_ADOPTAPATAS_3.Repositories.Models;

public partial class Adopcion
{
    public int IdAdopcion { get; set; }

    public DateTime? FechaAdopcion { get; set; }

    public int? FkCanino { get; set; }

    public int? FkUsuario { get; set; }

    public virtual Canino? FkCaninoNavigation { get; set; }

    public virtual Usuario? FkUsuarioNavigation { get; set; }
}
