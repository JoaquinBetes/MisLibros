namespace MisLibros.Models.Dtos.Libro
{
    public class LibroSimpleDto
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Img_portada { get; set; }
    }
}
