using System;
using System.Collections.Generic;

namespace API_ADOPTAPATAS_3.Repositories.Models;

public partial class Donacion
{
    public int IdDonacion { get; set; }

    public DateTime? FechaDonacion { get; set; }

    public decimal? Monto { get; set; }

    public int? FkCanino { get; set; }

    public int? FkUsuario { get; set; }

    public virtual Canino? FkCaninoNavigation { get; set; }

    public virtual Usuario? FkUsuarioNavigation { get; set; }
}
