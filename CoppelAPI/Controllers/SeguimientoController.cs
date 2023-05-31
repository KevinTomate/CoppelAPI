using CoppelAPI.Models;
using CoppelAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoppelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguimientoController : ControllerBase
    {
        public SeguimientoController(CoppelContext context)
        {
            Context = context;
            repository = new Repository<Denuncium>(Context);
        }
        Repository<Denuncium> repository;
        public CoppelContext Context { get; }

        [HttpPost]
        public IActionResult PostObtenerReporte(Seguimiento seg)
        {
            var x = repository.Get(seg.Folio);
            if (x == null)
            {
                return Ok(new Denuncium());
            }
            if (x.Clave == seg.Clave)
            {
                return Ok(x);
            }
            return Forbid();
        }
    }
}
