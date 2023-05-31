namespace CoppelWeb.ViewModels
{
    public class DatosDenuncia
    {
        public int Folio { get; set; }

        public int? IdEmpresa { get; set; }

        public int? IdPais { get; set; }

        public int? IdEstado { get; set; }

        public int? NumeroCentro { get; set; }

        public bool? Anonima { get; set; }

        public string? Detalle { get; set; } = null!;

        public DateTime? Fecha { get; set; }

        public string? Clave { get; set; } = null!;

        public int? IdEstatus { get; set; }

        public string? NombreCompleto { get; set; } = null!;

        public string? CorreoElectronico { get; set; } = null!;

        public string? Telefono { get; set; } = null!;
    }
}
