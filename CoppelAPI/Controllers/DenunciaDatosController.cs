using CoppelAPI.Models;
using CoppelAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoppelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DenunciaDatosController : ControllerBase
    {
        public DenunciaDatosController(CoppelContext context)
        {
            Context = context;
            repository = new Repository<Denunciadato>(Context);
        }

        public CoppelContext Context { get; }
        Repository<Denunciadato> repository;

        [HttpGet]
        public IEnumerable<Denunciadato> Get()
        {
            return Context.Denunciadatos;
        }

        [HttpGet("{folio}")]
        public IActionResult GetByFolio(int folio)
        {
            var x = repository.Get(folio);
            if (x == null)
            {
                return Ok(new Denunciadato());
            }
            return Ok(x);
        }
    }
}
