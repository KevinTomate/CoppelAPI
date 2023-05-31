using CoppelAPI.Models;
using CoppelAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoppelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        public EmpresaController(CoppelContext context)
        {
            Context = context;
            repository = new Repository<Empresa>(Context);
        }

        public CoppelContext Context { get; }
        Repository<Empresa> repository;

        [HttpGet]
        public IEnumerable<Empresa> Get()
        {
            return Context.Empresas;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var x = repository.Get(id);
            if (x == null)
            {
                return Ok(new Empresa());
            }
            return Ok(x);
        }
    }
}
