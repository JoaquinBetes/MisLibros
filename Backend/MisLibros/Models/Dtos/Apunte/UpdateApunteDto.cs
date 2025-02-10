namespace MisLibros.Models.Dtos.Apunte
{
    public class UpdateApunteDto
    {
        public required string Titulo { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Texto { get; set; }
        public int IdLibro { get; set; }
    }
}
