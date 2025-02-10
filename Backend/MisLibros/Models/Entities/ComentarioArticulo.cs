namespace MisLibros.Models.Entities
{
    public class ComentarioArticulo
    {
        // ID del usuario que comenta el articulo
        public required string IdUsuarioComentario { get; set; }
        //ID del articulo que se comenta
        public required string IdUsuarioArticulo { get; set; }
        public required int IdLibro { get; set; }
        public required string TituloApunte { get; set; }
        // atributo de la relación
        public string? Texto { get; set; }
        // Propiedades de navegación
        public Usuario Usuario { get; set; } = null!;
        public Articulo Articulo { get; set; } = null!;
    }
}
