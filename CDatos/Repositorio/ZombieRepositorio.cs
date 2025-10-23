using CDatos.Data;
using CDatos.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDatos.Repositorio
{
    public class ZombieRepositorio : IZombieRepositorio
    {
        private readonly DataContext _context;
        public ZombieRepositorio(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Zombie>> ObtenerZombies()
        {
            return await _context.Zombie.ToListAsync();
        }
        public async Task<Zombie> ObtenerZombiePorId(int id)
        {
            return await _context.Zombie.FindAsync(id);
        }
        public async Task<Zombie> CrearZombie(Zombie zombie)
        {
            _context.Zombie.Add(zombie);
            await _context.SaveChangesAsync();
            return zombie;
        }
        public void ActualizarZombie(Zombie zombie)
        {
            var zombieExistente = _context.Zombie.Find(zombie.Id);
            if (zombieExistente == null)
            {
                throw new Exception("Zombie no encontrada.");
            }
            zombieExistente.Nombre = zombie.Nombre;
            zombieExistente.NivelPeligro = zombie.NivelPeligro;
            zombieExistente.Tipo = zombie.Tipo;
            zombieExistente.Edad = zombie.Edad;
            zombieExistente.Velocidad = zombie.Velocidad;

            _context.SaveChanges();
        }
        public void EliminarZombie(int id)
        {
            var zombieExistente = _context.Zombie.Find(id);
            if (zombieExistente == null)
            {
                throw new Exception("Zombie no encontrada.");
            }
            zombieExistente.Estado = "Eliminado";

            _context.SaveChanges();
        }
    }
}
