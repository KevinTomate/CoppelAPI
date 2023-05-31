using CoppelAPI.Models;
using CoppelAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoppelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        public EstadoController(CoppelContext context)
        {
            Context = context;
            repository = new Repository<Estado>(Context);
        }

        public CoppelContext Context { get; }
        Repository<Estado> repository;

        [HttpGet]
        public IEnumerable<Estado> Get([FromQuery(Name = "pais")] int idPais)
        {
            if (idPais != 0 )
            {
                return Context.Estados.Where(e => e.IdPais == idPais);
            }
            return Context.Estados;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var x = repository.Get(id);
            if (x == null)
            {
                return Ok(new Estado());
            }
            return Ok(x);
        }
    }
}
