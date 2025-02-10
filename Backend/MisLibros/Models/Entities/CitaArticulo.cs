using System.ComponentModel.DataAnnotations;

namespace MisLibros.Models.Entities
{
    public class CitaArticulo
    {
        public int CitaId { get; set; } // FK hacia Cita
        public required string ArticuloIdUsuario { get; set; } // FK hacia Articulo 
        public int ArticuloIdLibro { get; set; } // FK hacia Articulo 
        [MaxLength(450)]
        public required string ArticuloTituloApunte { get; set; } // FK hacia Articulo 

        // Propiedades de navegación
        public Cita Cita { get; set; } = null!;
        public Articulo Articulo { get; set; } = null!;
    }
}
