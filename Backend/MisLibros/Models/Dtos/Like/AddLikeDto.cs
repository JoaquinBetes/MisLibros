namespace MisLibros.Models.Dtos.Like
{
    public class AddLikeDto
    {
        public required string IdUsuarioComentario { get; set; }
        public required string IdUsuarioArticulo { get; set; }
        public required int IdLibro { get; set; }
        public required string TituloApunte { get; set; }

    }
}
