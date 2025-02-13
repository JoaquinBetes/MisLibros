namespace MisLibros.Models.Dtos.Editorial
{
    public class EditorialSimpleDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public string? Web { get; set; }
    }
}
