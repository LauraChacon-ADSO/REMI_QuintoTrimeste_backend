using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_REMI_WebApi.Datos;
using Proyecto_REMI_WebApi.Models;
using Proyecto_REMI_WebApi.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_REMI_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoriasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public categoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<categoria>>> Getcategorias()
        {
            var categorias = await _context.categorias.ToListAsync();
            return Ok(categorias);
        }

        // GET: api/categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<categoria>> Getcategoria(int id)
        {
            var categoria = await _context.categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // PUT: api/categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcategoria(int id, EditarCategoriaDto editarCategoriaDto)
        {

            var categoria = await _context.categorias.FindAsync(id);
            if (categoria == null) return NotFound();

            categoria.nombreCategorias = editarCategoriaDto.nombreCategorias;

            await _context.SaveChangesAsync();
            return NoContent();

        }

        // POST: api/categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<categoria>> Postcategoria(CrearCategoriaDTO crearCategoriaDTO)
        {


            var nombreExistente = await _context.categorias
             .AnyAsync(c => c.nombreCategorias == crearCategoriaDTO.nombreCategorias);

            if (nombreExistente)
            {
                return BadRequest("El nombre de la categoria ya existe ya existe");
            }

            var nuevaCategoria = new categoria
            {

                nombreCategorias = crearCategoriaDTO.nombreCategorias,

            };

            _context.categorias.Add(nuevaCategoria);
            await _context.SaveChangesAsync();


            return CreatedAtAction("Getcategoria", new { id = nuevaCategoria.nombreCategorias }, nuevaCategoria);
        }


        // DELETE: api/categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletecategoria(int id)
        {
            var categoria = await _context.categorias
         .Include(c => c.subCategoria)
             .ThenInclude(sc => sc.productos)
         .FirstOrDefaultAsync(c => c.codigoCategorias == id);

            if (categoria == null)
                return NotFound();

            var productosConRestrict = categoria.subCategoria
           .SelectMany(sc => sc.productos)
           .Where(p => p.documentoProveedorNavigation != null)
           .ToList();

            if (productosConRestrict.Any())
            {
                return BadRequest("No se puede eliminar la categoría porque hay productos asociados a proveedores que impiden la eliminación.");
            }


            _context.categorias.Remove(categoria);

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {

                Console.WriteLine(ex.InnerException?.Message);
                return BadRequest("Ocurrió un error al eliminar la categoría.");
            }
        }
    }
}
