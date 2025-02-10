using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisLibros.Data;
using MisLibros.Models.Dtos.ComentarioArticulo;
using MisLibros.Models.Entities;

namespace MisLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioArticuloController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ComentarioArticuloController( ApplicationDbContext dbContext )
        {
            this._context = dbContext;
        }

        [HttpGet]
        [Route("articulo/{idUsuario}/{idLibro}/{tituloApunte}")]
        public async Task<IActionResult> GetAllComentariosByArticulo(string idUsuario, int idLibro, string tituloApunte)
        {
            var comentarios = await _context.Comentarios
                .Where(c => c.IdUsuarioArticulo == idUsuario && c.IdLibro == idLibro && c.TituloApunte == tituloApunte)
                .ToListAsync();
            return Ok(comentarios);
        }
        [HttpGet]
        [Route("{idUsuarioComentario}/{idUsuarioArticulo}/{idLibro}/{TituloApunte}")]
        public async Task<IActionResult> GetComentario(string idUsuarioComentario, string idUsuarioArticulo, int idLibro, string tituloApunte)
        {
            var comentario = await _context.Comentarios.FindAsync(idUsuarioComentario, idUsuarioArticulo, idLibro, tituloApunte);
            if (comentario == null)
            {
                return NotFound();
            }
            return Ok(comentario);
        }
        [HttpPost]
        public async Task<IActionResult> AddComentario( AddComentarioArticuloDto comentario)
        {
            var nuevoComentario = new ComentarioArticulo
            {
                IdUsuarioComentario = comentario.IdUsuarioComentario,
                IdUsuarioArticulo = comentario.IdUsuarioArticulo,
                IdLibro = comentario.IdLibro,
                TituloApunte = comentario.TituloApunte,
                Texto = comentario.Texto
            };
            _context.Comentarios.Add(nuevoComentario);
            await _context.SaveChangesAsync();
            return Ok(comentario);
        }
        [HttpPut]
        [Route("{idUsuarioComentario}/{idUsuarioArticulo}/{idLibro}/{TituloApunte}")]
        public async Task<IActionResult> UpdateComentario(string idUsuarioComentario, string idUsuarioArticulo, int idLibro, string tituloApunte, string texto)
        {
            var comentario = await _context.Comentarios.FindAsync(idUsuarioComentario, idUsuarioArticulo, idLibro, tituloApunte);
            if (comentario == null)
            {
                return NotFound();
            }
            comentario.Texto = texto;

            _context.Comentarios.Update(comentario);
            await _context.SaveChangesAsync();

            return Ok(comentario);
        }
        [HttpDelete]
        [Route("{idUsuarioComentario}/{idUsuarioArticulo}/{idLibro}/{TituloApunte}")]
        public async Task<IActionResult> DeleteComentario(string idUsuarioComentario, string idUsuarioArticulo, int idLibro, string tituloApunte)
        {
            var comentario = await _context.Comentarios.FindAsync(idUsuarioComentario, idUsuarioArticulo, idLibro, tituloApunte);
            if (comentario == null)
            {
                return NotFound();
            }
            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
