using System.ComponentModel.DataAnnotations.Schema;

namespace MisLibros.Models.Entities
{
    public class Escritor
    {
        [key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string NombreCompleto { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public DateOnly? FechaMuerte { get; set; }
        public string? ImgAutor { get; set; }
        public string? UrlBiografia { get; set; }

        public ICollection<Genero> Generos { get; set; } = new List<Genero>();
        public ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}
