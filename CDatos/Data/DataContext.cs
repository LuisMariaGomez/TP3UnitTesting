using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;

namespace CDatos.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }
        public DataContext() { }
        public DbSet<Zombie> Zombie { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-83HBC25;Initial Catalog=ZombieDB;Integrated Security=True;TrustServerCertificate=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Zombie>().HasData(
                new Zombie { Id = 1, Nombre = "Rickon", Edad = 24, NivelPeligro = "Bajo", Tipo = "Caminante", Velocidad = 3.2, FechaInfeccion = new DateTime(2024, 9, 15), Estado = "Vivo" },
                new Zombie { Id = 2, Nombre = "Luna", Edad = 30, NivelPeligro = "Medio", Tipo = "Corredor", Velocidad = 7.1, FechaInfeccion = new DateTime(2024, 9, 25), Estado = "Vivo" },
                new Zombie { Id = 3, Nombre = "Brutus", Edad = 45, NivelPeligro = "Alto", Tipo = "Mutante", Velocidad = 2.5, FechaInfeccion = new DateTime(2024, 8, 1), Estado = "Eliminado" },
                new Zombie { Id = 4, Nombre = "Marla", Edad = 27, NivelPeligro = "Medio", Tipo = "Caminante", Velocidad = 4.0, FechaInfeccion = new DateTime(2024, 10, 1), Estado = "Vivo" },
                new Zombie { Id = 5, Nombre = "Gnasher", Edad = 51, NivelPeligro = "Alto", Tipo = "Radioactivo", Velocidad = 1.8, FechaInfeccion = new DateTime(2024, 6, 30), Estado = "Eliminado" },
                new Zombie { Id = 6, Nombre = "Corvus", Edad = 32, NivelPeligro = "Medio", Tipo = "Corredor", Velocidad = 8.0, FechaInfeccion = new DateTime(2024, 8, 10), Estado = "Vivo" },
                new Zombie { Id = 7, Nombre = "Elsa", Edad = 19, NivelPeligro = "Bajo", Tipo = "Caminante", Velocidad = 2.9, FechaInfeccion = new DateTime(2024, 10, 5), Estado = "Vivo" },
                new Zombie { Id = 8, Nombre = "Tor", Edad = 60, NivelPeligro = "Alto", Tipo = "Mutante", Velocidad = 2.0, FechaInfeccion = new DateTime(2024, 4, 12), Estado = "Eliminado" },
                new Zombie { Id = 9, Nombre = "Sable", Edad = 34, NivelPeligro = "Medio", Tipo = "Corredor", Velocidad = 6.3, FechaInfeccion = new DateTime(2024, 9, 10), Estado = "Vivo" },
                new Zombie { Id = 10, Nombre = "Vera", Edad = 29, NivelPeligro = "Bajo", Tipo = "Caminante", Velocidad = 3.5, FechaInfeccion = new DateTime(2024, 10, 8), Estado = "Vivo" },
                new Zombie { Id = 11, Nombre = "Hawk", Edad = 38, NivelPeligro = "Alto", Tipo = "Mutante", Velocidad = 2.8, FechaInfeccion = new DateTime(2024, 7, 15), Estado = "Eliminado" },
                new Zombie { Id = 12, Nombre = "Dina", Edad = 25, NivelPeligro = "Medio", Tipo = "Corredor", Velocidad = 7.5, FechaInfeccion = new DateTime(2024, 9, 20), Estado = "Vivo" },
                new Zombie { Id = 13, Nombre = "Kragg", Edad = 41, NivelPeligro = "Alto", Tipo = "Radioactivo", Velocidad = 1.5, FechaInfeccion = new DateTime(2024, 5, 1), Estado = "Eliminado" },
                new Zombie { Id = 14, Nombre = "Milo", Edad = 22, NivelPeligro = "Bajo", Tipo = "Caminante", Velocidad = 3.1, FechaInfeccion = new DateTime(2024, 10, 3), Estado = "Vivo" },
                new Zombie { Id = 15, Nombre = "Rosa", Edad = 35, NivelPeligro = "Medio", Tipo = "Corredor", Velocidad = 6.9, FechaInfeccion = new DateTime(2024, 9, 1), Estado = "Vivo" },
                new Zombie { Id = 16, Nombre = "Xar", Edad = 50, NivelPeligro = "Alto", Tipo = "Mutante", Velocidad = 2.3, FechaInfeccion = new DateTime(2024, 2, 10), Estado = "Eliminado" },
                new Zombie { Id = 17, Nombre = "Ivy", Edad = 31, NivelPeligro = "Medio", Tipo = "Caminante", Velocidad = 4.2, FechaInfeccion = new DateTime(2024, 9, 12), Estado = "Vivo" },
                new Zombie { Id = 18, Nombre = "Nox", Edad = 40, NivelPeligro = "Alto", Tipo = "Radioactivo", Velocidad = 1.9, FechaInfeccion = new DateTime(2024, 3, 18), Estado = "Eliminado" },
                new Zombie { Id = 19, Nombre = "Kira", Edad = 28, NivelPeligro = "Bajo", Tipo = "Corredor", Velocidad = 5.8, FechaInfeccion = new DateTime(2024, 10, 11), Estado = "Vivo" },
                new Zombie { Id = 20, Nombre = "Omen", Edad = 36, NivelPeligro = "Alto", Tipo = "Mutante", Velocidad = 2.6, FechaInfeccion = new DateTime(2024, 6, 5), Estado = "Eliminado" }
            );
        }

    }
}
