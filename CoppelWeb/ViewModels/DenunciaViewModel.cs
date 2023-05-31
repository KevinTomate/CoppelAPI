
using CoppelWeb.Models;

namespace CoppelWeb.ViewModels
{
    public class DenunciaViewModel
    {
        public Pai[] Paises { get; set; }
        public Empresa[] Empresas { get; set; }
        public Estado[] Estados { get; set; }
        public Denuncium Denuncia { get; set; }
    }
}
