using CoppelAPI.Models;
using CoppelAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoppelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        public ComentarioController(CoppelContext context)
        {
            Context = context;
            repository = new Repository<Comentario>(Context);
        }

        public CoppelContext Context { get; }
        Repository<Comentario> repository;

        [HttpGet]
        public IEnumerable<Comentario> Get([FromQuery(Name = "folio")] int folio)
        {
            if (folio != 0)
            {
                return Context.Comentarios.Where(x=>x.Folio == folio);
            }
            return Context.Comentarios;
        }

        [HttpGet("{id}")]
        public IActionResult GetByFolio(int id)
        {
            var x = repository.Get(id);
            if (x == null)
            {
                return Ok(new Comentario());
            }
            return Ok(x);
        }
    }
}
