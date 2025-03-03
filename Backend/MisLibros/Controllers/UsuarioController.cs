using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisLibros.Data;
using MisLibros.Models.Dtos.Usuario;
using MisLibros.Models.Entities;
using MisLibrosAPI.Models.Dtos.Usuario;

namespace MisLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UsersController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            try
            {
                var user = new Usuario
                {
                    UserName = model.NombreUsuario,
                    Email = model.Email,
                    NombreCompleto = model.NombreCompleto,
                    Rol = model.Rol
                };

                // Guarda el usuario y genera un hash para la contraseña
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok(new { message = "Usuario registrado exitosamente." });
                }
                else if (result.Errors.Any(e => e.Code == "DuplicateUserName"))
                {
                    return BadRequest(new { message = "El nombre de usuario ya esta registrado.", details = result.Errors });
                }
                if (result.Errors.Any(e => e.Code == "DuplicateEmail"))
                {
                    return BadRequest(new { message = "El email ya esta registrado.", details = result.Errors });
                }

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            if (users == null || !users.Any())
            {
                return NotFound("No hay usuarios registrados.");
            }

            var userDtos = users.Select(u => new
            {
                u.Id, u.UserName, u.Email, u.NombreCompleto, u.Rol
            }).ToList();

            return Ok(userDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.NombreCompleto,
                user.Rol
            });
        }

        [HttpGet]
        [Route("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }
            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.NombreCompleto,
                user.Rol
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            // Buscar el usuario por email (ya que usas email para login)
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            // Verificar la contraseña (Identity se encarga de hacer el hash y compararlo)
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            // Aquí podrías generar y retornar un JWT o establecer cookies para la sesión,
            // pero por el ejemplo devolveremos simplemente un mensaje de éxito.
            return Ok(new { message = "Login exitoso." });
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto model)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            user.UserName = model.NombreUsuario ?? user.UserName;
            user.Email = model.Email ?? user.Email;
            user.NombreCompleto = model.NombreCompleto ?? user.NombreCompleto;
            user.Rol = model.Rol ?? user.Rol;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok("Usuario actualizado exitosamente.");
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok("Usuario eliminado exitosamente.");
            }

            return BadRequest(result.Errors);
        }

    }
}
