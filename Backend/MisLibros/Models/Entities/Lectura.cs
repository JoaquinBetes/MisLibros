using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisLibros.Models.Entities
{
    [PrimaryKey(nameof(IdLibro), nameof(IdEditorial), nameof(IdUsuario) )]
    public class Lectura
    {
        [ForeignKey(nameof(Usuario))]
        public required string IdUsuario { get; set; }
        public required int IdLibro { get; set; }
        public required int IdEditorial { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public int? Puntaje { get; set; }
        public Biblioteca Biblioteca { get; set; } = null!; // Propiedad de navegación
        public Usuario Usuario { get; set; } = null!; // Propiedad de navegación
    }
}
