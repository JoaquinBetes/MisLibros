using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisLibros.Data;
using MisLibros.Models.Dtos.Apunte;
using MisLibros.Models.Entities;

namespace MisLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApunteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ApunteController( ApplicationDbContext dbContext )
        {
            this._context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApuntes()
        {
            var apuntes = await _context.Apuntes.ToListAsync();
            return Ok(apuntes);
        }
        [HttpGet]
        [Route("{id}/{titulo}")]
        public async Task<IActionResult> GetApunteById(int id, string titulo)
        {
            var apunte = await _context.Apuntes.FindAsync([id, titulo]);
            if (apunte is null)
            {
                return NotFound();
            }
            return Ok(apunte);
        }
        [HttpPost]
        public async Task<IActionResult> AddApunte(AddApunteDto addApunteDto)
        {
            var apunte = new Apunte()
            {
                Titulo = addApunteDto.Titulo,
                Fecha = addApunteDto.Fecha,
                Texto = addApunteDto.Texto,
                IdLibro = addApunteDto.IdLibro
            };

            await _context.Apuntes.AddAsync(apunte);
            await _context.SaveChangesAsync();

            return Ok(apunte);
        }
        [HttpPut]
        [Route("{id}/{titulo}")]
        public async Task<IActionResult> UpdateApunte(int id, string titulo, UpdateApunteDto updateApunteDto)
        {
            var apunte = await _context.Apuntes.FindAsync([id, titulo]);

            if (apunte is null)
            {
                return NotFound();
            }

            apunte.Titulo = updateApunteDto.Titulo;
            apunte.Fecha = updateApunteDto.Fecha;
            apunte.Texto = updateApunteDto.Texto;
            apunte.IdLibro = updateApunteDto.IdLibro;

            _context.Apuntes.Update(apunte);
            await _context.SaveChangesAsync();

            return Ok(apunte);
        }
        [HttpDelete]
        [Route("{id}/{titulo}")]
        public async Task<IActionResult> DeleteApunte(int id, string titulo)
        {
            var apunte = await _context.Apuntes.FindAsync([id, titulo]);
            if (apunte is null)
            {
                return NotFound();
            }
            _context.Apuntes.Remove(apunte);
            await _context.SaveChangesAsync();
            return Ok(apunte);
        }
    }
}
