namespace MisLibros.Models.Dtos.Lectura
{
    public class AddLecturaDto
    {
        public required string IdUsuario { get; set; }
        public required int IdLibro { get; set; }
        public required int IdEditorial { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public int? Puntaje { get; set; }
    }
}
