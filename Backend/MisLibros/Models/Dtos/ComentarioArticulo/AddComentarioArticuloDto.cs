namespace MisLibros.Models.Dtos.ComentarioArticulo
{
    public class AddComentarioArticuloDto
    {
        public required string IdUsuarioComentario { get; set; }
        public required string IdUsuarioArticulo { get; set; }
        public required int IdLibro { get; set; }
        public required string TituloApunte { get; set; }
        public string? Texto { get; set; }
    }
}
