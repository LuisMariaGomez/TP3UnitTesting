using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using CNegocio.Logica;
using CDatos.Repositorio.IRepositorio;
using Shared.Entities;
using Shared.DTOs;

namespace TP3UnitTesting.Tests
{
    public class ZombieLogicaTests
    {
        private readonly Mock<IZombieRepositorio> _repoMock;
        private readonly ZombieLogica _logic;

        public ZombieLogicaTests()
        {
            _repoMock = new Mock<IZombieRepositorio>(MockBehavior.Strict);
            _logic = new ZombieLogica(_repoMock.Object);
        }

        [Fact]
        public async Task ObtenerZombies_MapsEntitiesToDtos()
        {
            // Arrange
            var zombies = new List<Zombie>
            {
                new Zombie { Id = 1, Nombre = "A", Edad = 20, NivelPeligro = "Bajo", Tipo = "Caminante", Velocidad = 3.2, FechaInfeccion = DateTime.Today, Estado = "Vivo" },
                new Zombie { Id = 2, Nombre = "B", Edad = 30, NivelPeligro = "Medio", Tipo = "Corredor", Velocidad = 6.5, FechaInfeccion = DateTime.Today, Estado = "Vivo" }
            };
            _repoMock.Setup(r => r.ObtenerZombies()).ReturnsAsync(zombies);

            // Act
            var dtos = await _logic.ObtenerZombies();

            // Assert
            Assert.Equal(2, dtos.Count);
            Assert.Contains(dtos, d => d.Id == 1 && d.Nombre == "A");
            Assert.Contains(dtos, d => d.Id == 2 && d.Nombre == "B");

            _repoMock.VerifyAll();
        }

        [Fact]
        public async Task ObtenerZombiePorId_InvalidId_ThrowsArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _logic.ObtenerZombiePorId(0));
        }

        [Fact]
        public async Task ObtenerZombiePorId_NotFound_ThrowsArgumentException()
        {
            _repoMock.Setup(r => r.ObtenerZombiePorId(5)).ReturnsAsync((Zombie?)null);

            await Assert.ThrowsAsync<ArgumentException>(() => _logic.ObtenerZombiePorId(5));

            _repoMock.Verify(r => r.ObtenerZombiePorId(5), Times.Once);
        }

        [Fact]
        public async Task CrearZombie_Null_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _logic.CrearZombie(null!));
        }

        [Fact]
        public async Task CrearZombie_InvalidNombre_ThrowsArgumentException()
        {
            var dto = new ZombieDTO
            {
                Nombre = " ", Edad = 10, NivelPeligro = "Bajo", Tipo = "Caminante", Velocidad = 1, FechaInfeccion = DateTime.Today
            };

            await Assert.ThrowsAsync<ArgumentException>(() => _logic.CrearZombie(dto));
        }

        [Fact]
        public async Task CrearZombie_Valid_CallsRepositorioCrearZombie_And_SetsEstadoVivo()
        {
            var dto = new ZombieDTO
            {
                Nombre = "Rick", Edad = 25, NivelPeligro = "Medio", Tipo = "Corredor", Velocidad = 5.5, FechaInfeccion = DateTime.Today
            };

            _repoMock.Setup(r => r.CrearZombie(It.Is<Zombie>(z =>
                z.Nombre == "Rick" &&
                z.Edad == 25 &&
                string.Equals(z.NivelPeligro, "Medio", StringComparison.OrdinalIgnoreCase) &&
                string.Equals(z.Tipo, "Corredor", StringComparison.OrdinalIgnoreCase) &&
                Math.Abs(z.Velocidad - 5.5) < 0.0001 &&
                z.Estado == "Vivo"
            ))).ReturnsAsync(new Zombie { Id = 100 });

            await _logic.CrearZombie(dto);

            _repoMock.Verify(r => r.CrearZombie(It.IsAny<Zombie>()), Times.Once);
        }

        [Fact]
        public async Task ActualizarZombie_Null_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _logic.ActualizarZombie(null!));
        }

        [Fact]
        public async Task ActualizarZombie_InvalidId_ThrowsArgumentException()
        {
            var dto = new ZombieDTO { Id = 0, Nombre = "X", Edad = 10, NivelPeligro = "Bajo", Tipo = "Caminante", Velocidad = 1, FechaInfeccion = DateTime.Today };
            await Assert.ThrowsAsync<ArgumentException>(() => _logic.ActualizarZombie(dto));
        }

        [Fact]
        public async Task ActualizarZombie_NotFound_ThrowsArgumentException()
        {
            var dto = new ZombieDTO { Id = 42, Nombre = "X", Edad = 10, NivelPeligro = "Bajo", Tipo = "Caminante", Velocidad = 1, FechaInfeccion = DateTime.Today };
            _repoMock.Setup(r => r.ObtenerZombiePorId(42)).ReturnsAsync((Zombie?)null);

            await Assert.ThrowsAsync<ArgumentException>(() => _logic.ActualizarZombie(dto));

            _repoMock.Verify(r => r.ObtenerZombiePorId(42), Times.Once);
        }

        [Fact]
        public async Task ActualizarZombie_Valid_PreservesEstadoWhenDtoEstadoEmpty_And_CallsRepositorioActualizar()
        {
            var existente = new Zombie
            {
                Id = 10, Nombre = "Old", Edad = 40, NivelPeligro = "Alto", Tipo = "Mutante", Velocidad = 2.2, FechaInfeccion = DateTime.Today.AddDays(-10), Estado = "Eliminado"
            };

            var dto = new ZombieDTO
            {
                Id = 10,
                Nombre = "NewName",
                Edad = 41,
                NivelPeligro = "Alto",
                Tipo = "Mutante",
                Velocidad = 2.2,
                FechaInfeccion = existente.FechaInfeccion,
                Estado = "" // empty -> should preserve existente.Estado
            };

            _repoMock.Setup(r => r.ObtenerZombiePorId(10)).ReturnsAsync(existente);
            _repoMock.Setup(r => r.ActualizarZombie(It.Is<Zombie>(z =>
                z.Id == 10 &&
                z.Nombre == "NewName" &&
                z.Estado == "Eliminado" // preserved
            )));

            await _logic.ActualizarZombie(dto);

            _repoMock.Verify(r => r.ObtenerZombiePorId(10), Times.Once);
            _repoMock.Verify(r => r.ActualizarZombie(It.IsAny<Zombie>()), Times.Once);
        }

        [Fact]
        public async Task EliminarZombie_InvalidId_ThrowsArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _logic.EliminarZombie(0));
        }

        [Fact]
        public async Task EliminarZombie_NotFound_ThrowsArgumentException()
        {
            _repoMock.Setup(r => r.ObtenerZombiePorId(99)).ReturnsAsync((Zombie?)null);

            await Assert.ThrowsAsync<ArgumentException>(() => _logic.EliminarZombie(99));

            _repoMock.Verify(r => r.ObtenerZombiePorId(99), Times.Once);
        }

        [Fact]
        public async Task EliminarZombie_Valid_CallsRepositorioEliminar()
        {
            var existente = new Zombie { Id = 8, Nombre = "ToDelete", Edad = 10, NivelPeligro = "Bajo", Tipo = "Caminante", Velocidad = 1, FechaInfeccion = DateTime.Today, Estado = "Vivo" };
            _repoMock.Setup(r => r.ObtenerZombiePorId(8)).ReturnsAsync(existente);
            _repoMock.Setup(r => r.EliminarZombie(8));

            await _logic.EliminarZombie(8);

            _repoMock.Verify(r => r.ObtenerZombiePorId(8), Times.Once);
            _repoMock.Verify(r => r.EliminarZombie(8), Times.Once);
        }
    }
}
