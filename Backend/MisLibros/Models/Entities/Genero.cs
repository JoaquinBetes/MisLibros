using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisLibros.Models.Entities
{
    public class Genero
    {
        [key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public required string Descripcion { get; set; }

        public ICollection<Escritor> Escritores { get; set; } = new List<Escritor>();
        public ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}
