using System;
using System.Collections.Generic;

namespace TrebolService.Models;

public partial class Grimorio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int NumHojas { get; set; }

    public DateTime FechaOp { get; set; }

    public int? UsuarioOp { get; set; }

    public virtual ICollection<GrimorioAsignado> GrimorioAsignados { get; } = new List<GrimorioAsignado>();
}
