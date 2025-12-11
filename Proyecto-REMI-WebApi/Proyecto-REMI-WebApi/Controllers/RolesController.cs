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
    }
}
