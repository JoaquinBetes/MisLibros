namespace MisLibros.Models.Dtos.Cita
{
    public class AddCitaDto
    {
        public int IdLibro { get; set; }
        public int IdEditorial { get; set; }
        public string? Texto { get; set; }
        public int? Pagina { get; set; }
        public string? Tipo { get; set; }
        public int Importancia { get; set; }
    }
}
