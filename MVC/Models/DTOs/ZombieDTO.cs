using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.DTOs
{
    public class ZombieDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Range(0, 100, ErrorMessage = "La edad debe estar entre 0 y 100.")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "El nivel de peligro es obligatorio.")]
        public string NivelPeligro { get; set; }    // Bajo, Medio, Alto

        [Required(ErrorMessage = "El tipo es obligatorio.")]
        public string Tipo { get; set; }            // Caminante, Corredor, Mutante, Radioactivo

        [Range(0, 100, ErrorMessage = "La velocidad debe estar entre 0 y 100.")]
        public double Velocidad { get; set; }       // En km/h

        public DateTime FechaInfeccion { get; set; } = DateTime.Now;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [Required(ErrorMessage = "El estado es obligatorio.")]
        public string? Estado { get; set; }
    }
}
