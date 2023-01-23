using System;
using System.Collections.Generic;

namespace TrebolService.Models;

public partial class Estudiante
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Identificacion { get; set; } = null!;

    public int Edad { get; set; }

    public int IdAfinidad { get; set; }

    public DateTime FechaOp { get; set; }

    public int? UsuarioOp { get; set; }

    public virtual GrimorioAsignado? GrimorioAsignado { get; set; }
}
