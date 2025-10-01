using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_REMI_WebApi.Datos;
using Proyecto_REMI_WebApi.Models;
using Proyecto_REMI_WebApi.Models.DTO_s;
using Proyecto_REMI_WebApi.Services;
using System.Security.Claims;

namespace Proyecto_REMI_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly JwtHelper _jwtHelper;

        public AuthController(ApplicationDbContext context, IConfiguration configuration, JwtHelper jwtHelper)
        {
            _context = context;
            _configuration = configuration;
            _jwtHelper = jwtHelper;
        }

        // LOGIN
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(u => u.documentoUsuario == loginDto.Documento);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, usuario.password))
                return Unauthorized("Documento o contraseña incorrectos");

            string rol = usuario.codigoNivel == 1 ? "Admin" : "Vendedor";
            string token = _jwtHelper.GenerateToken(usuario.documentoUsuario, rol);

            return Ok(new
            {
                token,
                rol,
                nombre = usuario.nombreUsuario,
                documento = usuario.documentoUsuario
            });
        }

        //REGISTER 
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var existente = await _context.usuarios
                .FirstOrDefaultAsync(u => u.documentoUsuario == dto.Documento || u.correoUsuario == dto.Correo);

            if (existente != null)
                return BadRequest("Ya existe un usuario con ese documento o correo");

            var usuario = new usuario
            {
                documentoUsuario = dto.Documento,
                tipoDocumentoUsuario = dto.TipoDocumento,
                nombreUsuario = dto.Nombre,
                apellidoUsuario = dto.Apellido,
                correoUsuario = dto.Correo,
                password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                codigoNivel = dto.CodigoNivel
            };

            _context.usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            string rol = usuario.codigoNivel == 1 ? "Admin" : "Vendedor";
            string token = _jwtHelper.GenerateToken(usuario.documentoUsuario, rol);

            return Ok(new
            {
                token,
                rol,
                nombre = usuario.nombreUsuario
            });
        }

        // LISTAR USUARIOS
        [HttpGet("usuarios")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<IEnumerable<ListarUsuariosDto>>> ListarUsuarios()
        {
            var usuarios = await _context.usuarios
                .Select(u => new ListarUsuariosDto
                {
                    DocumentoUsuario = u.documentoUsuario,
                    TipoDocumentoUsuario = u.tipoDocumentoUsuario,
                    NombreUsuario = u.nombreUsuario,
                    ApellidoUsuario = u.apellidoUsuario,
                    CorreoUsuario = u.correoUsuario,
                    CodigoNivel = u.codigoNivel
                })
                .ToListAsync();

            if (usuarios == null || !usuarios.Any())
                return NotFound("No se encontraron usuarios.");

            return Ok(usuarios);
        }

        // ACTUALIZAR USUARIO
        [HttpPut("usuarios/{documento}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ActualizarUsuario(string documento, [FromBody] ActualizarUsuariosDto dto)
        {
            var usuario = await _context.usuarios.FirstOrDefaultAsync(u => u.documentoUsuario == documento);
            if (usuario == null) return NotFound($"Usuario con documento '{documento}' no encontrado.");

            if (!string.IsNullOrEmpty(dto.CorreoUsuario) && dto.CorreoUsuario != usuario.correoUsuario)
            {
                var emailExistente = await _context.usuarios.AnyAsync(u => u.correoUsuario == dto.CorreoUsuario && u.documentoUsuario != documento);
                if (emailExistente)
                    return BadRequest("El correo electrónico ya está en uso por otro usuario.");

                usuario.correoUsuario = dto.CorreoUsuario;
            }

            usuario.tipoDocumentoUsuario = dto.TipoDocumentoUsuario;
            usuario.nombreUsuario = dto.NombreUsuario;
            usuario.apellidoUsuario = dto.ApellidoUsuario;
            usuario.codigoNivel = dto.CodigoNivel;

            if (!string.IsNullOrEmpty(dto.Password))
                usuario.password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _context.SaveChangesAsync();
            return Ok(new { message = $"Usuario con documento '{documento}' actualizado correctamente." });
        }

        // ELIMINAR USUARIO 
        [HttpDelete("usuarios/{documento}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> EliminarUsuario(string documento)
        {
            var usuario = await _context.usuarios.FirstOrDefaultAsync(u => u.documentoUsuario == documento);
            if (usuario == null) return NotFound($"Usuario con documento '{documento}' no encontrado.");

            _context.usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Usuario con documento '{documento}' eliminado correctamente." });
        }
    }
}
