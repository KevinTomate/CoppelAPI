using System;
using System.Collections.Generic;

namespace CoppelWeb.Models;

public partial class Pai
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Denuncium> Denuncia { get; set; } = new List<Denuncium>();

    public virtual ICollection<Estado> Estados { get; set; } = new List<Estado>();
}
