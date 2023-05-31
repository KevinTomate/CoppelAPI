using System;
using System.Collections.Generic;

namespace CoppelWeb.Models;

public partial class Comentario
{
    public int Id { get; set; }

    public int? Folio { get; set; }

    public string Comentario1 { get; set; } = null!;
}
