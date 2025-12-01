using Microsoft.AspNetCore.Mvc;
using Proyecto_REMI_WebApi.Datos;
using Proyecto_REMI_WebApi.Models;
using Proyecto_REMI_WebApi.Models.DTO_s;
using System.Linq;

namespace Proyecto_REMI_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _context.niveles
                .Select(n => new RolesDto
                {
                    CodigoNivel = n.codigoNivel,
                    NombreNivel = n.nombreNivel
                })
                .ToList();

            return Ok(roles);
        }

        // GET: api/Roles/{id}
        [HttpGet("{id}")]
        public IActionResult GetRolById(int id)
        {
            var rol = _context.niveles
                .Where(n => n.codigoNivel == id)
                .Select(n => new RolesDto
                {
                    CodigoNivel = n.codigoNivel,
                    NombreNivel = n.nombreNivel
                })
                .FirstOrDefault();

            if (rol == null)
                return NotFound(new { mensaje = "Rol no encontrado" });

            return Ok(rol);
        }

        // POST: api/Roles
        [HttpPost]
        public IActionResult CreateRol([FromBody] RolesDto nuevoRol)
        {
            if (nuevoRol == null || string.IsNullOrEmpty(nuevoRol.NombreNivel))
                return BadRequest(new { mensaje = "Datos del rol inválidos" });

            var rol = new niveles
            {
                nombreNivel = nuevoRol.NombreNivel
            };

            _context.niveles.Add(rol);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRolById), new { id = rol.codigoNivel }, nuevoRol);
        }

        // PUT: api/Roles/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateRol(int id, [FromBody] RolesDto rolActualizado)
        {
            var rol = _context.niveles.FirstOrDefault(n => n.codigoNivel == id);
            if (rol == null)
                return NotFound(new { mensaje = "Rol no encontrado" });

            if (string.IsNullOrEmpty(rolActualizado.NombreNivel))
                return BadRequest(new { mensaje = "El nombre del rol no puede estar vacío" });

            rol.nombreNivel = rolActualizado.NombreNivel;
            _context.SaveChanges();

            return Ok(new { mensaje = "Rol actualizado correctamente" });
        }

        // DELETE: api/Roles/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteRol(int id)
        {
            var rol = _context.niveles.FirstOrDefault(n => n.codigoNivel == id);
            if (rol == null)
                return NotFound(new { mensaje = "Rol no encontrado" });

            _context.niveles.Remove(rol);
            _context.SaveChanges();

            return Ok(new { mensaje = "Rol eliminado correctamente" });
        }
    }
}
