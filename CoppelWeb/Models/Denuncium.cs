using System;
using System.Collections.Generic;

namespace CoppelWeb.Models;

public partial class Denuncium
{
    public int Folio { get; set; }

    public int? IdEmpresa { get; set; }

    public int? IdPais { get; set; }

    public int? IdEstado { get; set; }

    public int NumeroCentro { get; set; }

    public sbyte Anonima { get; set; }

    public string Detalle { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string Clave { get; set; } = null!;

    public int? IdEstatus { get; set; }

    public string? NombreCompleto { get; set; }

    public string? Telefono { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Comentario { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual Estatus? IdEstatusNavigation { get; set; }

    public virtual Pai? IdPaisNavigation { get; set; }
}
