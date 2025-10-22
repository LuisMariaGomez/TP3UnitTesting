using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDatos.Repositorio.IRepositorio
{
    public interface IZombieRepositorio
    {
        Task<List<Zombie>> ObtenerZombies();
        Task<Zombie> ObtenerZombiePorId(int id);
        Task<Zombie> CrearZombie(Zombie zombie);
        void ActualizarZombie(Zombie zombie);
        void EliminarZombie(int id);

    }
}
