using GimnasioApp.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GimnasioApp.UI.Controllers
{
    public class MembresiasActivasController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions;

        public MembresiasActivasController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        // GET: /MembresiasActivas
        public async Task<IActionResult> Index(string? identificacion = null)
        {
            ViewBag.Identificacion = identificacion;

            try
            {
                var client = _httpClientFactory.CreateClient("GimnasioApi");
                var url = "api/membresias/activas";
                if (!string.IsNullOrWhiteSpace(identificacion))
                    url += $"?identificacion={Uri.EscapeDataString(identificacion)}";

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Error al obtener las membresías activas.";
                    return View(new List<MembresiaActivaViewModel>());
                }

                var json = await response.Content.ReadAsStringAsync();
                var membresias = JsonSerializer.Deserialize<List<MembresiaActivaViewModel>>(json, _jsonOptions)
                                 ?? new List<MembresiaActivaViewModel>();

                return View(membresias);
            }
            catch (Exception)
            {
                TempData["Error"] = "No fue posible conectarse con el servicio de membresías. Intente nuevamente.";
                return View(new List<MembresiaActivaViewModel>());
            }
        }
    }
}
