namespace MisLibros.Models.Dtos.Escritor
{
    public class AddEscritorDto
    {
        public required string NombreCompleto { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public DateOnly? FechaMuerte { get; set; }
        public string? UrlBiografia { get; set; }
        public string? ImgAutor { get; set; }
    }
}
