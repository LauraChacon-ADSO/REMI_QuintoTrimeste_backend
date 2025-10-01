using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_REMI_WebApi.Datos;
using Proyecto_REMI_WebApi.Models.DTO_s;

namespace Proyecto_REMI_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PasswordController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Cambiar contraseña (usuario autenticado)
        [HttpPost("cambiar")]
        [Authorize]
        public async Task<IActionResult> CambiarContraseña([FromBody] CambiarContraseñaDto dto)
        {
            var userId = User.FindFirst("sub")?.Value;
            if (userId == null) return Unauthorized();

            var usuario = await _context.usuarios.FirstOrDefaultAsync(u => u.documentoUsuario == userId);
            if (usuario == null) return NotFound("Usuario no encontrado.");

            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, usuario.password))
                return BadRequest("Contraseña actual incorrecta.");

            usuario.password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            await _context.SaveChangesAsync();

            return Ok("Contraseña cambiada correctamente.");
        }

        //Olvidé contraseña (solicitar token)
        [HttpPost("olvido")]
        [AllowAnonymous]
        public async Task<IActionResult> OlvidoContraseña([FromBody] OlvidoContraseñaDto dto)
        {
            var usuario = await _context.usuarios.FirstOrDefaultAsync(u => u.correoUsuario == dto.Correo);
            if (usuario == null) return BadRequest("Correo no registrado.");

            var token = Guid.NewGuid().ToString();
            usuario.ResetToken = token;
            usuario.ResetTokenExpiry = DateTime.UtcNow.AddHours(1);
            await _context.SaveChangesAsync();

            // En producción, enviar token por correo
            return Ok(new { ResetToken = token });
        }

        // Reseteamos la contraseña (usamos token temporal)
        [HttpPost("reset")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetContraseña([FromBody] ResetContraseñaDto dto)
        {
            var usuario = await _context.usuarios.FirstOrDefaultAsync(u => u.correoUsuario == dto.Email);
            if (usuario == null) return BadRequest("Correo no registrado.");

            if (usuario.ResetToken != dto.Token || usuario.ResetTokenExpiry < DateTime.UtcNow)
                return BadRequest("Token inválido o expirado.");

            usuario.password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            usuario.ResetToken = null;
            usuario.ResetTokenExpiry = null;
            await _context.SaveChangesAsync();

            return Ok("Contraseña restablecida correctamente.");
        }
    }
}
