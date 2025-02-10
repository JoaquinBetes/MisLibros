using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisLibros.Models.Entities
{
    public class Editorial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public string? Web { get; set; }
        public ICollection<Biblioteca> Biblioteca { get; set; } = new List<Biblioteca>();
    }
}
