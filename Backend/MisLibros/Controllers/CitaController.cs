using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisLibros.Data;
using MisLibros.Models.Dtos.Cita;
using MisLibros.Models.Entities;


namespace MisLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CitaController( ApplicationDbContext dbContext )
        {
            this._context = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCitas()
        {
            var citas = await _context.Citas.ToListAsync();
            return Ok(citas);
        }
        [HttpGet]
        [Route("libro/{idLibro}/editorial/{idEditorial}")]
        public async Task<IActionResult> GetCitasByLibroEditorial(int idLibro, int idEditorial)
        {
            var citas = await _context.Citas
                .Where(c => c.IdLibro == idLibro && c.IdEditorial == idEditorial)
                .ToListAsync();
            return Ok(citas);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCitaById(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita is null)
            {
                return NotFound();
            }
            return Ok(cita);
        }
        [HttpPost]
        public async Task<IActionResult> AddCita(AddCitaDto addCitaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Validar existencia del libro y editorial (Uso de Any es mas eficiente que find)
            var libroExiste = await _context.Bibliotecas.AnyAsync(l => 
                (l.IdLibro == addCitaDto.IdLibro) && (l.IdEditorial == addCitaDto.IdEditorial)
            );
            if (!libroExiste)
            {
                return NotFound("Libro o Editorial no encontrado");
            }

            var cita = new Cita
            {
                IdLibro = addCitaDto.IdLibro,
                IdEditorial = addCitaDto.IdEditorial,
                Texto = addCitaDto.Texto,
                Pagina = addCitaDto.Pagina,
                Tipo = addCitaDto.Tipo,
                Importancia = addCitaDto.Importancia
            };
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();
            return Ok(cita);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCita(UpdateCitaDto updateCitaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cita = await _context.Citas.FindAsync(updateCitaDto.Id);
            if (cita is null)
            {
                return NotFound();
            }
            cita.Texto = updateCitaDto.Texto;
            cita.Pagina = updateCitaDto.Pagina;
            cita.Tipo = updateCitaDto.Tipo;
            cita.Importancia = updateCitaDto.Importancia;
            await _context.SaveChangesAsync();
            return Ok(cita);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita is null)
            {
                return NotFound();
            }
            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();
            return Ok(cita);
        }   
    }
}
