using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisLibros.Data;
using MisLibros.Models.Dtos.Articulo;
using MisLibros.Models.Entities;

namespace MisLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ArticuloController( ApplicationDbContext dbContext )
        {
            this._context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticulos()
        {
            var articulos = await _context.Articulos.ToListAsync();
            return Ok(articulos);
        }

        [HttpGet]
        [Route("{idUsuario}/{idLibro}/{tituloApunte}")]
        public async Task<IActionResult> GetArticuloById(int idUsuario, int idLibro, string tituloApunte)
        {
            var articulo = await _context.Articulos.FindAsync([idUsuario, idLibro, tituloApunte]);
            if (articulo is null)
            {
                return NotFound();
            }
            return Ok(articulo);
        }

        [HttpGet]
        [Route("usuario/{idUsuario}")]
        public async Task<IActionResult> GetAllArticulosByUsuario(string idUsuario)
        {
            var articulos = await _context.Articulos.Where(a => a.IdUsuario == idUsuario).ToListAsync();
            return Ok(articulos);
        }

        [HttpGet]
        [Route("libro/{idLibro}")]
        public async Task<IActionResult> GetAllArticulosByLibro(int idLibro)
        {
            var articulos = await _context.Articulos.Where(a => a.IdLibro == idLibro).ToListAsync();
            return Ok(articulos);
        }

        [HttpGet]
        [Route("titulo/{tituloApunte}")]
        public async Task<IActionResult> GetAllArticulosByTitulo(string tituloApunte)
        {
            var articulos = await _context.Articulos.Where(a => a.TituloApunte == tituloApunte).ToListAsync();
            return Ok(articulos);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticulo(AddArticuloDto addArticuloDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Validar existencia del usuario
            var usuarioExiste = await _context.Users.AnyAsync(u => u.Id == addArticuloDto.IdUsuario);
            if (!usuarioExiste)
            {
                return NotFound("Usuario no encontrado");
            }
            // Validar si ya existe la relación
            var existeRelacion = await _context.Articulos
                .AnyAsync(a => a.IdUsuario == addArticuloDto.IdUsuario
                            && a.IdLibro == addArticuloDto.IdLibro
                            && a.TituloApunte == addArticuloDto.TituloApunte);
            if (existeRelacion)
            {
                return BadRequest("Ya existe la relación");
            }
            var articulo = new Articulo()
            {
                IdUsuario = addArticuloDto.IdUsuario,
                IdLibro = addArticuloDto.IdLibro,
                TituloApunte = addArticuloDto.TituloApunte
            };
            await _context.Articulos.AddAsync(articulo);
            await _context.SaveChangesAsync();
            return Ok(articulo);
        }
        [HttpDelete]
        [Route("{idUsuario}/{idLibro}/{tituloApunte}")]
        public async Task<IActionResult> DeleteArticulo(string idUsuario, int idLibro, string tituloApunte)
        {
            var articulo = await _context.Articulos.FindAsync([idUsuario, idLibro, tituloApunte]);
            if (articulo is null)
            {
                return NotFound();
            }
            _context.Articulos.Remove(articulo);
            await _context.SaveChangesAsync();
            return Ok(articulo);
        }
    }
}
