using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisLibros.Data;
using MisLibros.Models.Dtos.Like;
using MisLibros.Models.Entities;

namespace MisLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LikeController( ApplicationDbContext dbContext )
        {
            this._context = dbContext;
        }

        [HttpGet]
        [Route("articulo/{idUsuario}/{idLibro}/{tituloApunte}")]
        public async Task<IActionResult> GetNumberOfLikesByArticulo(string idUsuario, int idLibro, string tituloApunte)
        {
            var likes = await _context.Likes
                .Where(c => c.IdUsuarioArticulo == idUsuario && c.IdLibro == idLibro && c.TituloApunte == tituloApunte)
                .CountAsync();
            return Ok(likes);
        }

        [HttpPost]
        public async Task<ActionResult> AddLike(AddLikeDto like)
        {
            var nuevoLike = new Like
            {
                IdUsuarioComentario = like.IdUsuarioComentario,
                IdUsuarioArticulo = like.IdUsuarioArticulo,
                IdLibro = like.IdLibro,
                TituloApunte = like.TituloApunte
            };

            _context.Likes.Add(nuevoLike);
            await _context.SaveChangesAsync();

            return Ok(nuevoLike);
        }
        [HttpDelete]
        [Route("{idUsuarioComentario}/{idUsuarioArticulo}/{idLibro}/{TituloApunte}")]
        public async Task<ActionResult> DeleteLike(string idUsuarioComentario, string idUsuarioArticulo, int idLibro, string tituloApunte)
        {
            var like = await _context.Likes.FindAsync(idUsuarioComentario, idUsuarioArticulo, idLibro, tituloApunte);

            if (like == null)
            {
                return NotFound();
            }

            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
