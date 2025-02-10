using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisLibros.Data;
using MisLibros.Models.Dtos.Genero;
using MisLibros.Models.Entities;


namespace MisLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GeneroController( ApplicationDbContext dbContext )
        {
            this._context = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllGeneros()
        {
            return Ok(_context.Generos.ToList());
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGeneroById(int id)
        {
            var genero = await _context.Generos.FindAsync(id);
            if (genero is null)
            {
                return NotFound();
            }

            return Ok(genero);
        }

        [HttpPost]
        public async Task<IActionResult> AddGenero(AddGeneroDto addGeneroDto)
        {
            var generoEntity = new Genero()
            {
                Descripcion = addGeneroDto.Descripcion
            };

            await _context.Generos.AddAsync(generoEntity);
            await _context.SaveChangesAsync();
            return Ok(generoEntity);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateGenero( int id, UpdateGeneroDto updateGeneroDto )
        {
            var genero = await _context.Generos.FindAsync(id);
            if (genero is null)
            {
                return NotFound();
            }

            genero.Descripcion = updateGeneroDto.Descripcion;

            _context.Generos.Update(genero);
            await _context.SaveChangesAsync();
            return Ok(genero);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            var genero = await _context.Generos.FindAsync(id);
            if (genero is null)
            {
                return NotFound();
            }
            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
