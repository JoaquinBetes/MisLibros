using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisLibros.Data;
using MisLibros.Models.Dtos.Lectura;
using MisLibros.Models.Entities;

namespace MisLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LecturaController( ApplicationDbContext dbContext )
        {
            this._context = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLecturas()
        {
            var lecturas = await _context.Lecturas.ToListAsync();
            return Ok(lecturas);
        }
        [HttpGet]
        [Route("{idUsuario}/{idLibro}/{idEditorial}")]
        public async Task<IActionResult> GetLecturaById(int idUsuario, int idLibro, int idEditorial)
        {
            var lectura = await _context.Lecturas.FindAsync([idUsuario, idLibro, idEditorial]);
            if (lectura is null)
            {
                return NotFound();
            }
            return Ok(lectura);
        }
        [HttpGet]
        [Route("usuario/{idUsuario}")]
        public async Task<IActionResult> GetAllLecturasByUsuario(string idUsuario)
        {
            var lecturas = await _context.Lecturas
                .Where(l => l.IdUsuario == idUsuario)
                    .ToListAsync();
            return Ok(lecturas);
        }
        [HttpGet]
        [Route("libro/{idLibro}")]
        public async Task<IActionResult> GetAllLecturasByLibro(int idLibro)
        {
            var lecturas = await _context.Lecturas
                .Where(l => l.IdLibro == idLibro)
                    .ToListAsync();
            return Ok(lecturas);
        }
        [HttpGet]
        [Route("editorial/{idEditorial}")]
        public async Task<IActionResult> GetAllLecturasByEditorial(int idEditorial)
        {
            var lecturas = await _context.Lecturas
                .Where(l => l.IdEditorial == idEditorial)
                    .ToListAsync();
            return Ok(lecturas);
        }
        [HttpPost]
        public async Task<IActionResult> AddLectura( AddLecturaDto lecturaDto)
        {
            var lectura = new Lectura
            {
                IdUsuario = lecturaDto.IdUsuario,
                IdLibro = lecturaDto.IdLibro,
                IdEditorial = lecturaDto.IdEditorial,
                FechaInicio = lecturaDto.FechaInicio,
                FechaFin = lecturaDto.FechaFin,
                Puntaje = lecturaDto.Puntaje
            };
            await _context.Lecturas.AddAsync(lectura);
            await _context.SaveChangesAsync();
            return Ok(lectura);
        }
        [HttpPut]
        [Route("{idUsuario}/{idLibro}/{idEditorial}")]
        public async Task<IActionResult> UpdateLectura(string idUsuario, int idLibro, int idEditorial, AddLecturaDto lecturaDto)
        {
            var lectura = await _context.Lecturas.FindAsync([idUsuario, idLibro, idEditorial]);
            if (lectura is null)
            {
                return NotFound();
            }

            lectura.FechaInicio = lecturaDto.FechaInicio;
            lectura.FechaFin = lecturaDto.FechaFin;
            lectura.Puntaje = lecturaDto.Puntaje;

            _context.Lecturas.Update(lectura);
            await _context.SaveChangesAsync();
            return Ok(lectura);
        }
        [HttpDelete]
        [Route("{idUsuario}/{idLibro}/{idEditorial}")]
        public async Task<IActionResult> DeleteLectura(string idUsuario, int idLibro, int idEditorial)
        {
            var lectura = await _context.Lecturas.FindAsync([idUsuario, idLibro, idEditorial]);
            if (lectura is null)
            {
                return NotFound();
            }
            _context.Lecturas.Remove(lectura);
            await _context.SaveChangesAsync();
            return Ok(lectura);
        }
    }
}
