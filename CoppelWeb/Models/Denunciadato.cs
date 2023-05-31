using System;
using System.Collections.Generic;

namespace CoppelWeb.Models;

public partial class Denunciadato
{
    public int Folio { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public string Telefono { get; set; } = null!;
}
