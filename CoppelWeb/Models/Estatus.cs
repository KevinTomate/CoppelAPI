using System;
using System.Collections.Generic;

namespace CoppelWeb.Models;

public partial class Estatus
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Denuncium> Denuncia { get; set; } = new List<Denuncium>();
}
