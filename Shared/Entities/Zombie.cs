using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Zombie
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string NivelPeligro { get; set; }    // Bajo, Medio, Alto
        public string Tipo { get; set; }            // Caminante, Corredor, Mutante, Radioactivo
        public double Velocidad { get; set; }       // En km/h
        public DateTime FechaInfeccion { get; set; } = DateTime.Now;
        public string Estado { get; set; }
    }
}
