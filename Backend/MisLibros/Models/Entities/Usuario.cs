using Microsoft.AspNetCore.Identity;

namespace MisLibros.Models.Entities
{
    public class Usuario : IdentityUser
    {
        public required string NombreCompleto { get; set; }
        public required string Rol { get; set; }
        public ICollection<Articulo> Articulos { get; set; } = null!; // Navegación inversa
        public ICollection<Lectura> Lecturas { get; set; } = null!; // Navegación inversa
        public ICollection<ComentarioArticulo> Comentarios { get; set; } = null!; // Navegación inversa
    }
}
