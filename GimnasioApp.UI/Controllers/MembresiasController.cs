using GimnasioApp.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GimnasioApp.UI.Controllers
{
    public class MembresiasController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly JsonSerializerOptions _jsonOptions;

        public MembresiasController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        // GET: /Membresias
        public async Task<IActionResult> Index(string? identificacion = null)
        {
            ViewBag.Identificacion = identificacion;

            try
            {
                var client = _httpClientFactory.CreateClient("GimnasioApi");
                var url = "api/membresias";
                if (!string.IsNullOrWhiteSpace(identificacion))
                    url += $"?identificacion={Uri.EscapeDataString(identificacion)}";

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Error al obtener las membresías.";
                    return View(new List<MembresiaListadoViewModel>());
                }

                var json = await response.Content.ReadAsStringAsync();
                var membresias = JsonSerializer.Deserialize<List<MembresiaListadoViewModel>>(json, _jsonOptions)
                                 ?? new List<MembresiaListadoViewModel>();

                return View(membresias);
            }
            catch (Exception)
            {
                TempData["Error"] = "No fue posible conectarse con el servicio de membresías. Intente nuevamente.";
                return View(new List<MembresiaListadoViewModel>());
            }
        }

        // GET: /Membresias/Detalle/5
        public async Task<IActionResult> Detalle(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("GimnasioApi");
                var response = await client.GetAsync($"api/membresias/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Membresía no encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                var json = await response.Content.ReadAsStringAsync();
                var membresia = JsonSerializer.Deserialize<MembresiaDetalleViewModel>(json, _jsonOptions);

                if (membresia == null)
                {
                    TempData["Error"] = "Membresía no encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                return View(membresia);
            }
            catch (Exception)
            {
                TempData["Error"] = "No fue posible conectarse con el servicio de membresías. Intente nuevamente.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: /Membresias/Agregar
        public IActionResult Agregar()
        {
            return View(new AgregarMembresiaViewModel());
        }

        // POST: /Membresias/Agregar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(AgregarMembresiaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var client = _httpClientFactory.CreateClient("GimnasioApi");
                var payload = new
                {
                    model.Identificacion,
                    model.Nombre,
                    model.Apellidos,
                    model.Telefono,
                    model.PrecioBaseMensual,
                    FechaDeInicio = model.FechaDeInicio.ToString("yyyy-MM-dd"),
                    model.TipoDePlan
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/membresias", content);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Error al agregar la membresía.";
                    return View(model);
                }

                TempData["Exito"] = "Membresía agregada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "No fue posible conectarse con el servicio de membresías. Intente nuevamente.";
                return View(model);
            }
        }

        // GET: /Membresias/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("GimnasioApi");
                var response = await client.GetAsync($"api/membresias/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Membresía no encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                var json = await response.Content.ReadAsStringAsync();
                var detalle = JsonSerializer.Deserialize<MembresiaDetalleViewModel>(json, _jsonOptions);

                if (detalle == null)
                {
                    TempData["Error"] = "Membresía no encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                var model = new EditarMembresiaViewModel
                {
                    Id = detalle.Id,
                    Identificacion = detalle.Identificacion,
                    Nombre = detalle.Nombre,
                    Apellidos = detalle.Apellidos,
                    Telefono = detalle.Telefono,
                    PrecioBaseMensual = detalle.PrecioBaseMensual,
                    TipoDePlan = detalle.TipoDePlanId
                };

                return View(model);
            }
            catch (Exception)
            {
                TempData["Error"] = "No fue posible conectarse con el servicio de membresías. Intente nuevamente.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: /Membresias/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, EditarMembresiaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var client = _httpClientFactory.CreateClient("GimnasioApi");
                var payload = new
                {
                    model.Nombre,
                    model.Apellidos,
                    model.Telefono,
                    model.PrecioBaseMensual,
                    model.TipoDePlan
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/membresias/{id}", content);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Error al editar la membresía.";
                    return View(model);
                }

                TempData["Exito"] = "Membresía editada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "No fue posible conectarse con el servicio de membresías. Intente nuevamente.";
                return View(model);
            }
        }
    }
}
