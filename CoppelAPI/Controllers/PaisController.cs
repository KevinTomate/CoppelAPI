using CoppelAPI.Models;
using CoppelAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoppelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        public PaisController(CoppelContext context)
        {
            Context = context;
            repository = new Repository<Pai>(Context);
        }

        Repository<Pai> repository;
        public CoppelContext Context { get; }

        [HttpGet]
        public IEnumerable<Pai> Get()
        {
            return Context.Pais;
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var x = repository.Get(id);
            if (x == null)
            {
                return Ok(new Pai());
            }
            return Ok(x);
        }
    }
}
