using CoppelAPI.Models;
using CoppelAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoppelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstatusController : ControllerBase
    {
        public EstatusController(CoppelContext context)
        {
            Context = context;
            repository = new Repository<Estatus>(Context);
        }

        public CoppelContext Context { get; }
        Repository<Estatus> repository;

        [HttpGet]
        public IEnumerable<Estatus> Get()
        {
            return Context.Estatuses;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var x = repository.Get(id);
            if (x == null)
            {
                return Ok(new Estatus());
            }
            return Ok(x);
        }
    }
}
