using System;
using System.Collections.Generic;

namespace TrebolService.Models;

public partial class Afinidade
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime FechaOp { get; set; }

    public int? UsuarioOp { get; set; }
}
