using System;
using System.Collections.Generic;

namespace TrebolService.Models;

public partial class GrimorioAsignado
{
    public int Id { get; set; }

    public int IdEstudiante { get; set; }

    public int IdGrimorio { get; set; }

    public DateTime FechaUpdate { get; set; }

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual Grimorio IdGrimorioNavigation { get; set; } = null!;
}
