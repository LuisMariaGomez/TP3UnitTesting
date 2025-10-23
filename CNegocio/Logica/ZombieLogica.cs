using CDatos.Repositorio;
using CDatos.Repositorio.IRepositorio;
using CNegocio.Logica.ILogica;
using Shared.DTOs;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNegocio.Logica
{
    public class ZombieLogica : IZombieLogica
    {
        private readonly IZombieRepositorio _zombieRepositorio;
        public ZombieLogica(IZombieRepositorio zombieRepositorio)
        {
            _zombieRepositorio = zombieRepositorio;
        }
        public async Task<List<ZombieDTO>> ObtenerZombies()
        {
            var zombies = await _zombieRepositorio.ObtenerZombies();
            return zombies.Select(z => new ZombieDTO
            {
                Id = z.Id,
                Nombre = z.Nombre,
                Edad = z.Edad,
                NivelPeligro = z.NivelPeligro,
                Tipo = z.Tipo,
                Velocidad = z.Velocidad,
                FechaInfeccion = z.FechaInfeccion,
                Estado = z.Estado
            }).ToList();
        }
        public async Task<ZombieDTO> ObtenerZombiePorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del zombie debe ser mayor que cero.");

            var z = await _zombieRepositorio.ObtenerZombiePorId(id);
            if (z == null)
                throw new ArgumentException($"No se encontró un zombie con el ID {id}");

            return new ZombieDTO
            {
                Id = z.Id,
                Nombre = z.Nombre,
                Edad = z.Edad,
                NivelPeligro = z.NivelPeligro,
                Tipo = z.Tipo,
                Velocidad = z.Velocidad,
                FechaInfeccion = z.FechaInfeccion,
                Estado = z.Estado
            };
        }
        public async Task CrearZombie(ZombieDTO z)
        {
            var zombie = new Zombie
            {
                Nombre = z.Nombre,
                Edad = z.Edad,
                NivelPeligro = z.NivelPeligro,
                Tipo = z.Tipo,
                Velocidad = z.Velocidad,
                FechaInfeccion = z.FechaInfeccion,
                Estado = "Vivo"
            };
            var nuevaCategoria = await _zombieRepositorio.CrearZombie(zombie);
        }
        public async Task ActualizarZombie(ZombieDTO z)
        {
            var zombie = new Zombie
            {
                Id = z.Id,
                Nombre = z.Nombre,
                Edad = z.Edad,
                NivelPeligro = z.NivelPeligro,
                Tipo = z.Tipo,
                Velocidad = z.Velocidad,
                FechaInfeccion = z.FechaInfeccion,
            };
            _zombieRepositorio.ActualizarZombie(zombie);
        }
        public async Task EliminarZombie(int id)
        {
            _zombieRepositorio.EliminarZombie(id);
        }
    }
}
