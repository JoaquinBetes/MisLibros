using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisLibros.Models.Entities
{
    [PrimaryKey(nameof(IdLibro), nameof(Titulo))]
    public class Apunte
    {
        [ForeignKey(nameof(Libro))]
        public required int IdLibro { get; set; }
        public required string Titulo { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Texto { get; set; }
        public Libro? Libro { get; set; } // Propiedad de navegación
        public ICollection<Articulo> Articulos { get; set; } = null!; //Navegación inversa

    }
}
