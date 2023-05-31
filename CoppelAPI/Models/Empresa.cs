using System;
using System.Collections.Generic;

namespace CoppelAPI.Models;

public partial class Empresa
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Denuncium> Denuncia { get; set; } = new List<Denuncium>();
}
