using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using CNegocio.Logica;
using CNegocio.Logica.ILogica;
using Shared.DTOs;


namespace TP3UnitTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZombiesController : ControllerBase
    {
        private readonly IZombieLogica _IZombieLogica;

        public ZombiesController(IZombieLogica IZombieLogica)
        {
            _IZombieLogica = IZombieLogica;
        }

        // GET: api/Zombies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZombieDTO>>> GetZombie()
        {
            return await _IZombieLogica.ObtenerZombies();
        }

        // GET: api/Zombies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZombieDTO>> GetZombie(int id)
        {
            var zombie = await _IZombieLogica.ObtenerZombiePorId(id);

            if (zombie == null)
            {
                return NotFound();
            }

            return zombie;
        }

        // PUT: api/Zombies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZombie(int id, ZombieDTO zombie)
        {
            if (id != zombie.Id)
            {
                return BadRequest();
            }

            await _IZombieLogica.ActualizarZombie(zombie);

            return NoContent();
        }

        // POST: api/Zombies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Zombie>> PostZombie(ZombieDTO zombie)
        {
            await _IZombieLogica.CrearZombie(zombie);

            return CreatedAtAction("GetZombie", new { id = zombie.Id }, zombie);
        }

        // DELETE: api/Zombies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZombie(int id)
        {
            var zombie = await _IZombieLogica.ObtenerZombiePorId(id);
            if (zombie == null)
            {
                return NotFound();
            }

            await _IZombieLogica.EliminarZombie(zombie.Id);

            return NoContent();
        }
    }
}
