using CDatos.Repositorio;
using CDatos.Repositorio.IRepositorio;
using CNegocio.Logica.ILogica;
using Shared.DTOs;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                throw new ArgumentException("El ID del zombie debe ser mayor que cero.", nameof(id));

            var z = await _zombieRepositorio.ObtenerZombiePorId(id);
            if (z == null)
                throw new ArgumentException($"No se encontró un zombie con el ID {id}", nameof(id));

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
            if (z == null)
                throw new ArgumentNullException(nameof(z));

            ValidarZombieDto(z, isUpdate: false);

            var zombie = new Zombie
            {
                Nombre = z.Nombre!.Trim(),
                Edad = z.Edad,
                NivelPeligro = z.NivelPeligro!,
                Tipo = z.Tipo!,
                Velocidad = z.Velocidad,
                FechaInfeccion = z.FechaInfeccion,
                Estado = "Vivo"
            };

            await _zombieRepositorio.CrearZombie(zombie);
        }

        public async Task ActualizarZombie(ZombieDTO z)
        {
            if (z == null)
                throw new ArgumentNullException(nameof(z));

            if (z.Id <= 0)
                throw new ArgumentException("El ID del zombie a actualizar debe ser mayor que cero.", nameof(z.Id));

            // Verificar existencia
            var existente = await _zombieRepositorio.ObtenerZombiePorId(z.Id);
            if (existente == null)
                throw new ArgumentException($"No existe un zombie con ID {z.Id} para actualizar.", nameof(z.Id));

            ValidarZombieDto(z, isUpdate: true);

            var zombie = new Zombie
            {
                Id = z.Id,
                Nombre = z.Nombre!.Trim(),
                Edad = z.Edad,
                NivelPeligro = z.NivelPeligro!,
                Tipo = z.Tipo!,
                Velocidad = z.Velocidad,
                FechaInfeccion = z.FechaInfeccion,
                Estado = string.IsNullOrWhiteSpace(z.Estado) ? existente.Estado : z.Estado
            };

            _zombieRepositorio.ActualizarZombie(zombie);
        }

        public async Task EliminarZombie(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del zombie a eliminar debe ser mayor que cero.", nameof(id));

            var existente = await _zombieRepositorio.ObtenerZombiePorId(id);
            if (existente == null)
                throw new ArgumentException($"No existe un zombie con ID {id} para eliminar.", nameof(id));

            _zombieRepositorio.EliminarZombie(id);
        }

        // Validaciones en la lógica de negocio
        private void ValidarZombieDto(ZombieDTO z, bool isUpdate)
        {
            // Nombre
            if (string.IsNullOrWhiteSpace(z.Nombre))
                throw new ArgumentException("El nombre es obligatorio.", nameof(z.Nombre));
            var nombre = z.Nombre.Trim();
            if (nombre.Length > 50)
                throw new ArgumentException("El nombre debe tener entre 2 y 50 caracteres.", nameof(z.Nombre));

            // Edad
            if (z.Edad < 0 || z.Edad > 120)
                throw new ArgumentException("La edad debe estar entre 0 y 120.", nameof(z.Edad));

            // NivelPeligro
            var niveles = new[] { "Bajo", "Medio", "Alto" };
            if (string.IsNullOrWhiteSpace(z.NivelPeligro) || !niveles.Any(n => string.Equals(n, z.NivelPeligro, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException($"NivelPeligro no válido. Valores permitidos: {string.Join(", ", niveles)}.", nameof(z.NivelPeligro));

            // Tipo
            var tipos = new[] { "Caminante", "Corredor", "Mutante", "Radioactivo" };
            if (string.IsNullOrWhiteSpace(z.Tipo) || !tipos.Any(t => string.Equals(t, z.Tipo, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException($"Tipo no válido. Valores permitidos: {string.Join(", ", tipos)}.", nameof(z.Tipo));

            // Velocidad
            if (double.IsNaN(z.Velocidad) || z.Velocidad < 0 || z.Velocidad > 100)
                throw new ArgumentException("La velocidad debe ser un valor numérico entre 0 y 100 km/h.", nameof(z.Velocidad));

            // FechaInfeccion
            if (z.FechaInfeccion > DateTime.Now)
                throw new ArgumentException("La fecha de infección no puede estar en el futuro.", nameof(z.FechaInfeccion));

            // Estado (solo validar si viene explícitamente en DTO para update)
            if (!isUpdate && !string.IsNullOrWhiteSpace(z.Estado))
            {
                var estados = new[] { "Vivo", "Eliminado" };
                if (!estados.Any(e => string.Equals(e, z.Estado, StringComparison.OrdinalIgnoreCase)))
                    throw new ArgumentException($"Estado no válido. Valores permitidos: {string.Join(", ", new[] { "Vivo", "Eliminado" })}.", nameof(z.Estado));
            }
            else if (isUpdate && !string.IsNullOrWhiteSpace(z.Estado))
            {
                var estados = new[] { "Vivo", "Eliminado" };
                if (!estados.Any(e => string.Equals(e, z.Estado, StringComparison.OrdinalIgnoreCase)))
                    throw new ArgumentException($"Estado no válido. Valores permitidos: {string.Join(", ", estados)}.", nameof(z.Estado));
            }
        }
    }
}
