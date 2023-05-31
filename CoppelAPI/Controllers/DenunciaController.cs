using CoppelAPI.Models;
using CoppelAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoppelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DenunciaController : ControllerBase
    {
        public DenunciaController(CoppelContext context)
        {
            Context = context;
            repository = new Repository<Denuncium>(Context);
        }
        Repository<Denuncium> repository;
        public CoppelContext Context { get; }

        [HttpGet]
        public IEnumerable<Denuncium> Get([FromQuery(Name = "estatus")] int idEstatus)
        {
            if (idEstatus != 0)
            {
                return Context.Denuncia.Where(x=> x.IdEstatus == idEstatus);
            }
            return Context.Denuncia.Include(x => x.IdEmpresaNavigation)
                                    .Include(x=>x.IdPaisNavigation)
                                    .Include(x=>x.IdEstadoNavigation)
                                    .Include(x=>x.IdEstatusNavigation);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var x = repository.Get(id);
            if (x == null)
            {
                return Ok(new Denuncium());
            }
            return Ok(x);
        }

        [HttpPost]
        public IActionResult Post(Denuncium denuncia)
        {
            int ab = 2 + 2;
            if (denuncia == null) return BadRequest();

            if (string.IsNullOrWhiteSpace(denuncia.Detalle))
            {
                ModelState.AddModelError("", "Debe proporcionar un detalle de la denuncia");
            }
            if (Context.Denuncia.Any(x => x.Folio == denuncia.Folio))
            {
                ModelState.AddModelError("", "El folio proporcionado ya se encuentra registrado");
            }
            if (ModelState.IsValid)
            {
                Context.Denuncia.Add(denuncia);
                Context.SaveChanges();
                return Ok(denuncia);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        [HttpPut]
        public IActionResult Put(Denuncium denuncia)
        {
            if (denuncia == null) return BadRequest();

            if (string.IsNullOrWhiteSpace(denuncia.Detalle))
            {
                ModelState.AddModelError("", "Debe proporcionar un detalle de la denuncia");
            }
            if (ModelState.IsValid)
            {
                Denuncium d = repository.Get(denuncia.Folio);
                d.Anonima = denuncia.Anonima;
                d.Detalle = denuncia.Detalle;
                d.Fecha = denuncia.Fecha;
                d.IdEmpresa = denuncia.IdEmpresa;
                d.IdEstado = denuncia.IdEstado;
                d.IdEstatus = denuncia.IdEstatus;
                d.IdPais = denuncia.IdPais;
                d.NumeroCentro = denuncia.NumeroCentro;
                d.Comentario = denuncia.Comentario;
                repository.Update(d);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        
    }
}
