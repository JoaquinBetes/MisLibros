using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisLibros.Models.Entities
{
    public class Articulo
    {
        // Clave foránea hacia Usuario
        [ForeignKey(nameof(Usuario))]
        public string? IdUsuario { get; set; }

        // Clave foránea compuesta hacia Apunte (Titulo + IdLibro)
        [MaxLength(450)]
        public string? TituloApunte { get; set; } 
        public int IdLibro { get; set; } 

        // Propiedades de navegación
        public Usuario Usuario { get; set; } = null!;
        public Apunte Apunte { get; set; } = null!;
        // Many to many a entidad intermedia con Cita
        public ICollection<CitaArticulo> CitaArticulos { get; set; } = new List<CitaArticulo>();
        public ICollection<ComentarioArticulo> Comentarios { get; set; } = new List<ComentarioArticulo>();

    }
}
    
