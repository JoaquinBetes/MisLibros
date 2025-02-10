using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisLibros.Data;
using MisLibros.Models.Dtos.Escritor;
using MisLibros.Models.Entities;

namespace MisLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscritorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EscritorController( ApplicationDbContext dbContext )
        {
            this._context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEscritores()
        {
            var escritores = await _context.Escritores.ToListAsync();

            return Ok(escritores);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEscritorById(int id)
        {
            var escritor = await _context.Escritores.FindAsync(id);
            if (escritor is null)
            {
                return NotFound();
            }
            return Ok(escritor);
        }

        [HttpPost]
        public async Task<IActionResult> AddEscritor( AddEscritorDto escritorDto )
        {
            var escritorEntity = new Escritor()
            {
                NombreCompleto = escritorDto.NombreCompleto,
                FechaNacimiento = escritorDto.FechaNacimiento,
                FechaMuerte = escritorDto.FechaMuerte,
                UrlBiografia = escritorDto.UrlBiografia,
                ImgAutor = escritorDto.ImgAutor
            };

            await _context.Escritores.AddAsync(escritorEntity);
            await _context.SaveChangesAsync();

            return Ok(escritorEntity);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateEscritor(int id, UpdateEscritorDto updateEscritorDto) 
        {
            var escritor = await _context.Escritores.FindAsync(id);
            if (escritor is null)
            {
                return NotFound();
            }
            escritor.NombreCompleto = updateEscritorDto.NombreCompleto;
            escritor.FechaNacimiento = updateEscritorDto.FechaNacimiento;
            escritor.FechaMuerte = updateEscritorDto.FechaMuerte;
            escritor.UrlBiografia = updateEscritorDto.UrlBiografia;
            escritor.ImgAutor = updateEscritorDto.ImgAutor;

            _context.Escritores.Update(escritor);
            await _context.SaveChangesAsync();
            return Ok(escritor);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEscritor(int id)
        {
            var escritor = await _context.Escritores.FindAsync(id);
            if (escritor is null)
            {
                return NotFound();
            }
            _context.Escritores.Remove(escritor);
            await _context.SaveChangesAsync();
            return Ok(escritor);
        }

    }
}
