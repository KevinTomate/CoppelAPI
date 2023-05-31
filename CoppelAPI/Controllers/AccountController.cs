using CoppelAPI.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace CoppelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(CoppelContext context)
        {
            Context = context;
        }

        public CoppelContext Context { get; }

        [HttpGet("username/{nombre}/{password}")]
        public string Login(string nombre, string password)
        {
            var cuenta = Context.Usuarios.FirstOrDefault(x => x.Nombre == nombre && x.Password == password);
            if (cuenta == null)
            {
                return null;
            }
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, nombre));
            claims.Add(new Claim(ClaimTypes.Role, "Administracion"));
            ClaimsIdentity identidad = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            string identidadJSON = JsonConvert.SerializeObject(identidad, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            return identidadJSON;
        }
    }
}
