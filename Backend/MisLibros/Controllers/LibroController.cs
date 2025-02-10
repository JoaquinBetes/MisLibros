using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisLibros.Data;
using MisLibros.Models.Dtos.Libro;
using MisLibros.Models.Entities;
using System.Runtime.InteropServices;

namespace MisLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LibroController(ApplicationDbContext dbContext)
        {
            this._context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLibros()
        {
            var libros = await _context.Libros.ToListAsync();

            return Ok(libros);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetLibroById(int id)
        {
            var libro = await _context.Libros.FindAsync(id);

            if (libro is null)
            {
                return NotFound();
            }

            return Ok(libro);
        }
        [HttpPost]
        public async Task<IActionResult> AddLibro(AddLibroDto addLibroDto)
        {
            var libro = new Libro()
            {
                Titulo = addLibroDto.Titulo,
                Descripcion = addLibroDto.Descripcion,
                img_portada = addLibroDto.img_portada
            };

            await _context.Libros.AddAsync(libro);
            await _context.SaveChangesAsync();

            return Ok(libro);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateLibro(int id, UpdateLibroDto updateLibroDto)
        {
            var libro = await _context.Libros.FindAsync(id);

            if (libro is null)
            {
                return NotFound();
            }

            libro.Titulo = updateLibroDto.Titulo;
            libro.Descripcion = updateLibroDto.Descripcion;
            libro.img_portada = updateLibroDto.img_portada;

            _context.Libros.Update(libro);
            await _context.SaveChangesAsync();

            return Ok(libro);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);

            if (libro is null) 
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return Ok(libro);
        }
    }
}
