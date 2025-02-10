using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisLibros.Models.Entities
{
    public class Cita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdLibro { get; set; }
        public int IdEditorial { get; set; }
        public string? Texto { get; set; }
        public int? Pagina { get; set; }
        public string? Tipo { get; set; }
        public int Importancia { get; set; }
        public Biblioteca Biblioteca { get; set; } = null!;
        // Many to many a entidad intermedia con Articulo
        public ICollection<CitaArticulo> CitaArticulos { get; set; } = new List<CitaArticulo>();
    }
}
