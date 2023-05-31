using CoppelWeb.Models;

namespace CoppelWeb.Areas.Administracion.ViewModels
{
    public class VistaViewModel
    {
        public Denuncium  Denuncia { get; set; }
        public Estatus[] Estados { get; set; }
    }
}
