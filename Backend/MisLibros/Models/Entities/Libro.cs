using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisLibros.Models.Entities
{
    public class Libro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? img_portada { get; set; }
        public ICollection<Apunte> Apuntes { get; set; } = new List<Apunte>();
        public ICollection<Genero> Generos { get; set; } = new List<Genero>();
        public ICollection<Escritor> Escritores { get; set; } = new List<Escritor>();
        public ICollection<Biblioteca> Biblioteca { get; set; } = new List<Biblioteca>();
    }
}
