using System;
using System.Collections.Generic;

namespace LogsAllianceApi.Models;

public partial class Historico
{
    public Guid Id { get; set; }

    public string IdUsuario { get; set; } = null!;

    public Guid? IdRelacionGuid { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string? Tipo { get; set; }

    public string? Descripcion { get; set; }

    public string? Evento { get; set; }

    public string? Mensaje { get; set; }

    public string? IdRelacionVarchar { get; set; }
}
