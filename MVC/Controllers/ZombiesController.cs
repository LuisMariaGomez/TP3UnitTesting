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

        // Listar todos los zombies
        public async Task<IActionResult> Index()
        {
            var zombies = await _httpClient.GetFromJsonAsync<List<ZombieDTO>>(_apiUrl);
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

        // Confirmar eliminación
        public async Task<IActionResult> Delete(int id)
        {
            var zombie = await _httpClient.GetFromJsonAsync<ZombieDTO>($"{_apiUrl}/{id}");
            if (zombie == null) return NotFound();
            return View(zombie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");
            return RedirectToAction(nameof(Index));
        }

    }
}