using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNegocio.Logica.ILogica
{
    public interface IZombieLogica
    {
        Task<List<ZombieDTO>> ObtenerZombies();
        Task<ZombieDTO> ObtenerZombiePorId(int id);
        Task CrearZombie(ZombieDTO z);
        Task ActualizarZombie(ZombieDTO z);
        Task EliminarZombie(int id);
    }
}
