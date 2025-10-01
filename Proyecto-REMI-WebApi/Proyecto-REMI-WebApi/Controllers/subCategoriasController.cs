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
    public class subCategoriasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public subCategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/subCategorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<subCategoria>>> GetsubCategorias()
        {
            var subCategorias = await _context.subCategorias.ToListAsync();
            return Ok(subCategorias);
        }

        // GET: api/subCategorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<subCategoria>> GetsubCategoria(int id)
        {
            var subCategoria = await _context.subCategorias.FindAsync(id);

            if (subCategoria == null)
            {
                return NotFound();
            }

            return subCategoria;
        }

        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<subCategoria>>> GetSubCategoriasByCategoria(int categoriaId)
        {
            var subcategorias = await _context.subCategorias
                .Where(sc => sc.codigoCategorias == categoriaId)
                .ToListAsync();

            if (!subcategorias.Any())
            {
                return NotFound("No se encontraron subcategorías para esta categoría.");
            }

            return Ok(subcategorias);
        }

        // PUT: api/subCategorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutsubCategoria(int id, EditarSubcategoriaDto editarSubcategoriaDto)
        {
            var subCategorias = await _context.subCategorias.FindAsync(id);
            if (subCategorias == null) return NotFound();

            subCategorias.nombreSubCategorias = editarSubcategoriaDto.nombreSubCategorias;


            await _context.SaveChangesAsync();
            return NoContent();

        }

        // POST: api/subCategorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<subCategoria>> PostsubCategoria(CrearSubcategoriaDTO crearSubcategoriaDTO)
        {
            var nombreExistente = await _context.subCategorias.AnyAsync(sc => sc.nombreSubCategorias ==
                   crearSubcategoriaDTO.nombreSubCategorias);

            if (nombreExistente)
            {

                return BadRequest("El nombre de la subcategoria ya existe ya existe");
            }
            var nuevaSubCategoria = new subCategoria
            {
                nombreSubCategorias = crearSubcategoriaDTO.nombreSubCategorias,
                codigoCategorias = crearSubcategoriaDTO.codigoCategorias
            };


            _context.subCategorias.Add(nuevaSubCategoria);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetsubCategoria", new { id = nuevaSubCategoria.codigoSubCategorias }, nuevaSubCategoria);


        }

        // DELETE: api/subCategorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletesubCategoria(int id)
        {
            var subCategoria = await _context.subCategorias.FindAsync(id);
            if (subCategoria == null)
            {
                return NotFound();
            }

            _context.subCategorias.Remove(subCategoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
