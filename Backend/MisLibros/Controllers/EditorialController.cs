using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisLibros.Data;
using MisLibros.Models.Dtos.Editorial;
using MisLibros.Models.Entities;

namespace MisLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorialController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EditorialController(ApplicationDbContext dbContext) 
        {
            this._context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlllEditoriales()
        {
            var editoriales = await _context.Editoriales.ToListAsync();
            return Ok(editoriales);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEditorialById(int id)
        { 
            var editorial = await _context.Editoriales.FindAsync(id);
            if (editorial is null)
            { 
                return NotFound();
            }
            return Ok(editorial);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditorial( AddEditorialDto addEditorialDto)
        {
            var editorial = new Editorial()
            {
                Nombre = addEditorialDto.Nombre,
                Web = addEditorialDto.Web
            };
            await _context.Editoriales.AddAsync(editorial);
            await _context.SaveChangesAsync();
            return Ok(editorial);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateEditorial( int id, UpdateEditorialDto updateEditorialDto )
        {
            var editorial = await _context.Editoriales.FindAsync(id);
            if ( editorial is null )
            {
                return NotFound();
            }
            editorial.Nombre = updateEditorialDto.Nombre;
            editorial.Web = updateEditorialDto.Web;
            _context.Editoriales.Update(editorial);
            await _context.SaveChangesAsync();
            return Ok(editorial);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEditorial(int id)
        {
            var editorial = await _context.Editoriales.FindAsync(id);
            if (editorial is null)
            {
                return NotFound();
            };
            _context.Editoriales.Remove(editorial);
            await _context.SaveChangesAsync();
            return Ok(editorial);
        }
    }
}
