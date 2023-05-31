using System;
using System.Collections.Generic;

namespace CoppelWeb.Models;

public partial class Estado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdPais { get; set; }

    public virtual ICollection<Denuncium> Denuncia { get; set; } = new List<Denuncium>();

    public virtual Pai IdPaisNavigation { get; set; } = null!;
}
