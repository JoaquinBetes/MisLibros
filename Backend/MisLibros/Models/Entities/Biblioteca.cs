using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MisLibros.Models.Entities
{
    public class Biblioteca
    {
        [ForeignKey(nameof(Libro))]
        public int IdLibro { get; set; }
        [ForeignKey(nameof(Editorial))]
        public int IdEditorial { get; set; }
        public int CantidadPaginas { get; set; }
        public Libro? Libro { get; set; }
        public Editorial? Editorial { get; set; }
        public ICollection<Lectura> Lecturas { get; set; } = null!; // Navegación inversa
        public ICollection<Cita> Citas { get; set; } = null!; // Many to many con Citas
    }
}
