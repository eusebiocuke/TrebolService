using System;
using System.Collections.Generic;

namespace TrebolService.Models;

public partial class Ingreso
{
    public int Id { get; set; }

    public DateTime Fechaop { get; set; }

    /// <summary>
    /// Status solicitud  PENDIENTE - ACEPTADA
    /// </summary>
    public string Status { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Identificacion { get; set; }

    public int? Edad { get; set; }

    public string? Motivo { get; set; }

    public int? IdAfinidad { get; set; }
}
