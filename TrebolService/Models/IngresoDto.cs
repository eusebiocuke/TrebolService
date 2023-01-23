using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrebolService.Models;

public partial class IngresoDto:Ingreso
{
    
    /// <summary>
    /// Status solicitud  PENDIENTE - ACEPTADA
    /// </summary>

    [Required]
    public new string? Nombre { get; set; }
    [Required]
    public new string? Apellido { get; set; }
    [Required]
    public new string? Identificacion { get; set; }
    [Required]
    [Range(0, 999)]
    public new int? Edad { get; set; }
    [Required]
    public new int? IdAfinidad { get; set; }
}

