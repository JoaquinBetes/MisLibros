using MisLibros.Models.Dtos.Libro;
using MisLibros.Models.Dtos.Editorial;

namespace MisLibros.Models.Dtos.Biblioteca
{
    public class BibliotecaDto
    {
        public int IdLibro { get; set; }
        public int IdEditorial { get; set; }
        public int CantidadPaginas { get; set; }
        public LibroSimpleDto? Libro { get; set; }
        public EditorialSimpleDto? Editorial { get; set; }
    }
}

