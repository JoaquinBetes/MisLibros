namespace MisLibros.Models.Dtos.Libro
{
    public class AddLibroDto
    {
        public required string Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? img_portada { get; set; }
    }
}
