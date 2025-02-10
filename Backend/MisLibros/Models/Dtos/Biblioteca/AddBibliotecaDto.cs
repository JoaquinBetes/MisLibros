using System.ComponentModel.DataAnnotations.Schema;

namespace MisLibros.Models.Dtos.Biblioteca
{
    public class AddBibliotecaDto
    {
        public int IdLibro { get; set; }
        public int IdEditorial { get; set; }
        public int CantidadPaginas { get; set; }
    }
}
