using CoppelWeb.Areas.Administracion.ViewModels;
using CoppelWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

namespace CoppelWeb.Areas.Administracion.Controllers
{
    [Authorize(Roles = "Administracion")]
    [Area("Administracion")]
    public class HomeController : Controller
    {
        public string URL { get; set; } = "https://localhost:7228";

        [Route("administracion")]
        [Route("administracion/Home")]
        [Route("administracion/Home/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Pendientes()
        {
            HttpClient httpClient = new HttpClient();
            string denunciasJSON = await httpClient.GetStringAsync($"{URL}/api/denuncia?estatus=1");
            IEnumerable < Denuncium > denuncias = JsonConvert.DeserializeObject<Denuncium[]>(denunciasJSON)!;
            return View(denuncias);
        }

        [HttpGet]
        public async Task<IActionResult> Finalizadas()
        {
            HttpClient httpClient = new HttpClient();
            string denunciasJSON = await httpClient.GetStringAsync($"{URL}/api/denuncia?estatus=2");
            IEnumerable<Denuncium> denuncias = JsonConvert.DeserializeObject<Denuncium[]>(denunciasJSON)!;
            return View(denuncias);
        }

        [HttpGet]
        public async Task<IActionResult> Canceladas()
        {
            HttpClient httpClient = new HttpClient();
            string denunciasJSON = await httpClient.GetStringAsync($"{URL}/api/denuncia?estatus=3");
            IEnumerable<Denuncium> denuncias = JsonConvert.DeserializeObject<Denuncium[]>(denunciasJSON)!;
            return View(denuncias);
        }

        [HttpGet]
        public async Task<IActionResult> Vista(int id)
        {
            if(id == 0)
            {
                return RedirectToAction("Index");
            }
            HttpClient httpClient = new HttpClient();
            string denunciaJSON = await httpClient.GetStringAsync($"{URL}/api/denuncia/{id}");
            Denuncium denuncia = JsonConvert.DeserializeObject<Denuncium>(denunciaJSON)!;
            string paisesJSON = await httpClient.GetStringAsync($"{URL}/api/pais/{denuncia.IdPais}");
            string empresasJSON = await httpClient.GetStringAsync($"{URL}/api/empresa/{denuncia.IdEmpresa}");
            string estadosJSON = await httpClient.GetStringAsync($"{URL}/api/estado/{denuncia.IdEstado}");
            string estatusJSON = await httpClient.GetStringAsync($"{URL}/api/estatus/{denuncia.IdEstatus}");
            denuncia.IdEmpresaNavigation = JsonConvert.DeserializeObject<Empresa>(empresasJSON)!;
            denuncia.IdPaisNavigation = JsonConvert.DeserializeObject<Pai>(paisesJSON)!;
            denuncia.IdEstadoNavigation = JsonConvert.DeserializeObject<Estado>(estadosJSON)!;
            denuncia.IdEstatusNavigation = JsonConvert.DeserializeObject<Estatus>(estatusJSON)!;

            string estatusAllJSON = await httpClient.GetStringAsync($"{URL}/api/estatus/");
            Estatus[] estatusAll = JsonConvert.DeserializeObject<Estatus[]>(estatusAllJSON)!;
            VistaViewModel vm = new VistaViewModel()
            {
                Denuncia = denuncia,
                Estados = estatusAll
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Vista(VistaComentarioViewModel vm)
        {
            HttpClient httpClient = new HttpClient();
            string denunciaJSON = await httpClient.GetStringAsync($"{URL}/api/denuncia/{vm.Folio}");
            Denuncium denuncia = JsonConvert.DeserializeObject<Denuncium>(denunciaJSON)!;
            denuncia.IdEstatus = vm.IdEstatus;
            denuncia.Comentario = vm.Comentario;

            string json = JsonConvert.SerializeObject(denuncia);

            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PutAsync($"{URL}/api/denuncia/", httpContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                string denJSON = await httpResponse.Content.ReadAsStringAsync();
                Denuncium den = JsonConvert.DeserializeObject<Denuncium>(denJSON)!;
                return RedirectToAction("Index");
            }
            string paisesJSON = await httpClient.GetStringAsync($"{URL}/api/pais/{denuncia.IdPais}");
            string empresasJSON = await httpClient.GetStringAsync($"{URL}/api/empresa/{denuncia.IdEmpresa}");
            string estadosJSON = await httpClient.GetStringAsync($"{URL}/api/estado/{denuncia.IdEstado}");
            string estatusJSON = await httpClient.GetStringAsync($"{URL}/api/estatus/{denuncia.IdEstatus}");
            denuncia.IdEmpresaNavigation = JsonConvert.DeserializeObject<Empresa>(empresasJSON)!;
            denuncia.IdPaisNavigation = JsonConvert.DeserializeObject<Pai>(paisesJSON)!;
            denuncia.IdEstadoNavigation = JsonConvert.DeserializeObject<Estado>(estadosJSON)!;
            denuncia.IdEstatusNavigation = JsonConvert.DeserializeObject<Estatus>(estatusJSON)!;

            string estatusAllJSON = await httpClient.GetStringAsync($"{URL}/api/estatus/");
            Estatus[] estatusAll = JsonConvert.DeserializeObject<Estatus[]>(estatusAllJSON)!;
            VistaViewModel vmV = new VistaViewModel()
            {
                Denuncia = denuncia,
                Estados = estatusAll
            };
            return View(vmV);
        }
    }
}
