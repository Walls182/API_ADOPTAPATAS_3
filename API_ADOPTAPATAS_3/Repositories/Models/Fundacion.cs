﻿using System;
using System.Collections.Generic;

namespace API_ADOPTAPATAS_3.Repositories.Models;

public partial class Fundacion
{
    public int IdFundacion { get; set; }

    public string? NombreRepresentante { get; set; }

    public string? NombreFundacion { get; set; }

    public string? Direccion { get; set; }

    public string? Municipio { get; set; }

    public string? Departamento { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public string? Celular { get; set; }

    public string? Descripcion { get; set; }

    public string? Mision { get; set; }

    public string? Vision { get; set; }

    public string? ObjetivoSocial { get; set; }

    public string? LogoFundacion { get; set; }

    public string? FotoFundacion { get; set; }

    public int? FkLogin { get; set; }

    public int? FkRol { get; set; }

    public int? FkEstado { get; set; }

    public virtual ICollection<Canino> Caninos { get; set; } = new List<Canino>();

    public virtual Estado? FkEstadoNavigation { get; set; }

    public virtual Login? FkLoginNavigation { get; set; }

    public virtual Rol? FkRolNavigation { get; set; }
}
