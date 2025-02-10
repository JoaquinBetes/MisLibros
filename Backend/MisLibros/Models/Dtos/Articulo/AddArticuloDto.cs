using System.ComponentModel.DataAnnotations;

namespace MisLibros.Models.Dtos.Articulo
{
    public class AddArticuloDto
    {
        public required string IdUsuario { get; set; }
        public required string TituloApunte { get; set; }
        public required int IdLibro { get; set; }
    }
}
