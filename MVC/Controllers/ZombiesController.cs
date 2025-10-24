using Microsoft.AspNetCore.Mvc;
using MVC.Models.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace MVC.Controllers
{
    public class ZombiesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7010/api/Zombies"; // Ajusta el puerto si es necesario

        public ZombiesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // Listar zombies (por defecto oculta los eliminados). Pasar ?showAll=true para mostrar todos.
        public async Task<IActionResult> Index(bool showAll = false)
        {
            var zombies = await _httpClient.GetFromJsonAsync<List<ZombieDTO>>(_apiUrl) ?? new List<ZombieDTO>();

            if (!showAll)
            {
                zombies = zombies
                    .Where(z => !string.Equals(z.Estado, "Eliminado", StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewData["ShowAll"] = showAll;
            return View(zombies);
        }

        // Mostrar detalles de un zombie
        public async Task<IActionResult> Details(int id)
        {
            var zombie = await _httpClient.GetFromJsonAsync<ZombieDTO>($"{_apiUrl}/{id}");
            if (zombie == null) return NotFound();
            return View(zombie);
        }

        // Formulario de creación
        public IActionResult Create()
        {
            return View();
        }

        // Crear zombie (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ZombieDTO zombie)
        {
            zombie.Estado = string.Empty; // sino vuela al querer mandarlo como null
            var response = await _httpClient.PostAsJsonAsync(_apiUrl, zombie);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return View(zombie);
        }

        // Formulario de edición
        public async Task<IActionResult> Edit(int id)
        {
            var zombie = await _httpClient.GetFromJsonAsync<ZombieDTO>($"{_apiUrl}/{id}");
            if (zombie == null) return NotFound();
            return View(zombie);
        }

        // Editar zombie (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ZombieDTO zombie)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiUrl}/{id}", zombie);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return View(zombie);
        }

        // Confirmar "eliminación" -> marcamos Estado = "Eliminado" y hacemos PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zombie = await _httpClient.GetFromJsonAsync<ZombieDTO>($"{_apiUrl}/{id}");
            if (zombie == null) return NotFound();

            zombie.Estado = "Eliminado";
            await _httpClient.PutAsJsonAsync($"{_apiUrl}/{id}", zombie);

            return RedirectToAction(nameof(Index));
        }

        // Reanimar (cambiar Estado = "Vivo") usando PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Revive(int id)
        {
            var zombie = await _httpClient.GetFromJsonAsync<ZombieDTO>($"{_apiUrl}/{id}");
            if (zombie == null) return NotFound();

            zombie.Estado = "Vivo";
            await _httpClient.PutAsJsonAsync($"{_apiUrl}/{id}", zombie);

            return RedirectToAction(nameof(Index));
        }
    }
}