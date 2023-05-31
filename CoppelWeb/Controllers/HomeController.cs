using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Security.Policy;

namespace CoppelWeb.Controllers
{
    public class HomeController : Controller
    {
        public string URL { get; set; } = "https://localhost:7228";
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Administracion" });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string nombre, string password)
        {
            HttpClient client = new HttpClient();
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Por favor ingrese nombre de usuario o contraseña");
                return View();
            }
            string cuenta = await client.GetStringAsync($"{URL}/api/Account/username/{nombre}/{password}");
            if (!string.IsNullOrWhiteSpace(cuenta))
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.Name, nombre));
                identity.AddClaim(new Claim(ClaimTypes.Role, "Administracion"));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
                return RedirectToAction("Index", "Home", new { area = "Administracion" });
            }
            ModelState.AddModelError("", "Usuario y/o contraseña incorrectos");
            return View();
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        public string AccessDenied()
        {
            return "Acceso Denegado";
        }
    }
}
