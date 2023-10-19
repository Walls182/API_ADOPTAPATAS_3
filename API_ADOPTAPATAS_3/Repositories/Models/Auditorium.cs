using System;
using System.Collections.Generic;

namespace API_ADOPTAPATAS_3.Repositories.Models;

public partial class Auditorium
{
    public int IdAuditoria { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Comentario { get; set; }

    public string? NombreAuditor { get; set; }
}
