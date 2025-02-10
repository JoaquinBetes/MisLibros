namespace MisLibros.Models.Dtos.Usuario
{
    public class UpdateUserDto
    {
        public required string NombreUsuario { get; set; }
        public required string Email { get; set; }
        public required string NombreCompleto { get; set; }
        public required string Rol { get; set; }
    }
}
