using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using Newtonsoft.Json;
using CoppelWeb.ViewModels;
using System.Text;
using CoppelWeb.Models;
using System.Net.Http;

namespace CoppelWeb.Controllers
{
    public class DenunciaController : Controller
    {
        public string URL { get; set; } = "https://localhost:7228";
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Agregar()
        {
            DenunciaViewModel vm = new DenunciaViewModel();
            HttpClient httpClient = new HttpClient();

            string paisesJSON = await httpClient.GetStringAsync($"{URL}/api/pais");
            string empresasJSON = await httpClient.GetStringAsync($"{URL}/api/empresa");
            string estadosJSON = await httpClient.GetStringAsync($"{URL}/api/estado");
            vm.Empresas = JsonConvert.DeserializeObject<Empresa[]>(empresasJSON)!;
            vm.Paises = JsonConvert.DeserializeObject<Pai[]>(paisesJSON)!;
            vm.Estados = JsonConvert.DeserializeObject<Estado[]>(estadosJSON)!;
            vm.Denuncia = new Denuncium();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(Denuncium m)
        {
            if(string.IsNullOrWhiteSpace(m.NombreCompleto) && string.IsNullOrWhiteSpace(m.Telefono) && string.IsNullOrWhiteSpace(m.CorreoElectronico))
                m.Anonima = 1;
            else
                m.Anonima = 0;
            m.IdEstatus = 1;
            HttpClient httpClient = new HttpClient();
            //if (m.NumeroCentro==0)
            //{
            //    ModelState.AddModelError("", "Proporcione un numero de Centro valido");
            //}
            //if (string.IsNullOrWhiteSpace(m.Detalle))
            //{
            //    ModelState.AddModelError("", "Por favor propocione un detalle de la denuncia");
            //}
            //if(m.Anonima == 0)
            //{
            //    if (string.IsNullOrWhiteSpace(m.NombreCompleto)){
            //        ModelState.AddModelError("", "Por favor propocione su nombre completo");
            //    }
            //    if (string.IsNullOrWhiteSpace(m.Telefono))
            //    {
            //        ModelState.AddModelError("", "Por favor propocione su teléfono");
            //    }
            //    if (string.IsNullOrWhiteSpace(m.CorreoElectronico))
            //    {
            //        ModelState.AddModelError("", "Por favor propocione su correo electrónico");
            //    }
            //}
            ///*if*/ (ModelState.IsValid)
            //{
                string json = JsonConvert.SerializeObject(m);

                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse = await httpClient.PostAsync($"{URL}/api/denuncia/", httpContent);
                if (httpResponse.IsSuccessStatusCode)
                {
                    string denJSON = await httpResponse.Content.ReadAsStringAsync();
                    Denuncium den = JsonConvert.DeserializeObject<Denuncium>(denJSON)!;
                    return RedirectToAction("Exito", den);
                }
                string message = await httpResponse.Content.ReadAsStringAsync();
                ModelState.AddModelError("", message);
            //}
            DenunciaViewModel vm = new DenunciaViewModel();

            string paisesJSON = await httpClient.GetStringAsync($"{URL}/api/pais");
            string empresasJSON = await httpClient.GetStringAsync($"{URL}/api/empresa");
            string estadosJSON = await httpClient.GetStringAsync($"{URL}/api/estado");
            vm.Empresas = JsonConvert.DeserializeObject<Empresa[]>(empresasJSON)!;
            vm.Paises = JsonConvert.DeserializeObject<Pai[]>(paisesJSON)!;
            vm.Estados = JsonConvert.DeserializeObject<Estado[]>(estadosJSON)!;
            vm.Denuncia = m;

            return View(vm);
        }

        [HttpGet]
        public IActionResult Exito(Denuncium den)
        {
            if(den!=null || den!=new Denuncium())
            {
                return View(den);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Seguimiento()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Seguimiento(Seguimiento seg)
        {
            HttpClient httpClient = new HttpClient();
            string json = JsonConvert.SerializeObject(seg);

            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync($"{URL}/api/seguimiento/", httpContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                string denJSON = await httpResponse.Content.ReadAsStringAsync();
                Denuncium den = JsonConvert.DeserializeObject<Denuncium>(denJSON)!;
                return RedirectToAction("Vista", den);
            }
            ModelState.AddModelError("", "La clave o el folio proporcionado no es válido");
            return View(seg);
        }

        [HttpGet]
        public async Task<IActionResult> Vista(Denuncium den)
        {
            HttpClient httpClient = new HttpClient();
            if (den == null)
            {
               return RedirectToAction("Seguimiento");
            }
            string paisesJSON = await httpClient.GetStringAsync($"{URL}/api/pais/{den.IdPais}");
            string empresasJSON = await httpClient.GetStringAsync($"{URL}/api/empresa/{den.IdEmpresa}");
            string estadosJSON = await httpClient.GetStringAsync($"{URL}/api/estado/{den.IdEstado}");
            string estatusJSON = await httpClient.GetStringAsync($"{URL}/api/estatus/{den.IdEstatus}");
            den.IdEmpresaNavigation = JsonConvert.DeserializeObject<Empresa>(empresasJSON)!;
            den.IdPaisNavigation = JsonConvert.DeserializeObject<Pai>(paisesJSON)!;
            den.IdEstadoNavigation = JsonConvert.DeserializeObject<Estado>(estadosJSON)!;
            den.IdEstatusNavigation = JsonConvert.DeserializeObject<Estatus>(estatusJSON)!;
            return View(den);
        }
    }
}
