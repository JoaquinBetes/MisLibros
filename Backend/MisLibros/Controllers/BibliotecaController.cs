using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MisLibros.Data;
using MisLibros.Models.Dtos.Biblioteca;
using MisLibros.Models.Entities;

namespace MisLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BibliotecaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BibliotecaController( ApplicationDbContext dbContext )
        {
            this._context = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBibliotecas()
        {
            var biblioteca = await _context.Bibliotecas.ToListAsync();
            return Ok(biblioteca);
        }
        [HttpGet]
        [Route("{idLibro}/{idEditorial}")]
        public async Task<IActionResult> GetBibliotecaById(int idLibro, int idEditorial)
        {
            var biblioteca = await _context.Bibliotecas.FindAsync([idLibro, idEditorial]);
            if (biblioteca is null)
            {
                return NotFound();
            }
            return Ok(biblioteca);
        }
        [HttpPost]
        public async Task<IActionResult> AddBiblioteca(AddBibliotecaDto addBibliotecaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar existencia del libro y editorial (Uso de Any es mas eficiente que find)
            var libroExiste = await _context.Libros.AnyAsync(l => l.Id == addBibliotecaDto.IdLibro);
            var editorialExiste = await _context.Editoriales.AnyAsync(e => e.Id == addBibliotecaDto.IdEditorial);

            if (!libroExiste || !editorialExiste)
            {
                return NotFound("Libro o Editorial no encontrado");
            }

            // Validar si ya existe la relación
            var existeRelacion = await _context.Bibliotecas
                .AnyAsync(b => b.IdLibro == addBibliotecaDto.IdLibro
                            && b.IdEditorial == addBibliotecaDto.IdEditorial);

            if (existeRelacion)
            {
                return Conflict("Esta relación ya existe");
            }

            var biblioteca = new Biblioteca()
            {
                IdLibro = addBibliotecaDto.IdLibro,
                IdEditorial = addBibliotecaDto.IdEditorial,
                CantidadPaginas = addBibliotecaDto.CantidadPaginas
            };

            await _context.Bibliotecas.AddAsync(biblioteca);
            await _context.SaveChangesAsync();

            return Ok(biblioteca);
        }
        [HttpPut]
        [Route("{idLibro}/{idEditorial}")]
        public async Task<IActionResult> UpdateBiblioteca(int idLibro, int idEditorial, UpdateBibliotecaDto updateBibliotecaDto)
        {
            var biblioteca = await _context.Bibliotecas.FindAsync([idLibro, idEditorial]);

            if (biblioteca is null)
            {
                return NotFound();
            }

            biblioteca.CantidadPaginas = updateBibliotecaDto.CantidadPaginas;

            _context.Bibliotecas.Update(biblioteca);
            await _context.SaveChangesAsync();

            return Ok(biblioteca);
        }
        [HttpDelete]
        [Route("{idLibro}/{idEditorial}")]
        public async Task<IActionResult> DeleteBiblioteca(int idLibro, int idEditorial)
        {
            var biblioteca = await _context.Bibliotecas.FindAsync([idLibro, idEditorial]);
            if (biblioteca is null)
            {
                return NotFound();
            }
            _context.Bibliotecas.Remove(biblioteca);
            await _context.SaveChangesAsync();
            return Ok(biblioteca);
        }
    }
}
