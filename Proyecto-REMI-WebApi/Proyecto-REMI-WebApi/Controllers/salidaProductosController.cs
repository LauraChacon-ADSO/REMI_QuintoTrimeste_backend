using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_REMI_WebApi.Datos;
using Proyecto_REMI_WebApi.Models;

namespace Proyecto_REMI_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class salidaProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public salidaProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/salidaProductos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<salidaProducto>>> GetsalidaProductos()
        {
            return await _context.salidaProductos.ToListAsync();
        }

        // GET: api/salidaProductos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<salidaProducto>> GetsalidaProducto(int id)
        {
            var salidaProducto = await _context.salidaProductos.FindAsync(id);

            if (salidaProducto == null)
            {
                return NotFound();
            }

            return salidaProducto;
        }

        // PUT: api/salidaProductos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutsalidaProducto(int id, salidaProducto salidaProducto)
        {
            if (id != salidaProducto.codigoReciboVenta)
            {
                return BadRequest();
            }

            _context.Entry(salidaProducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!salidaProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/salidaProductos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<salidaProducto>> PostsalidaProducto(salidaProducto salidaProducto)
        {
            _context.salidaProductos.Add(salidaProducto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (salidaProductoExists(salidaProducto.codigoReciboVenta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetsalidaProducto", new { id = salidaProducto.codigoReciboVenta }, salidaProducto);
        }

        // DELETE: api/salidaProductos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletesalidaProducto(int id)
        {
            var salidaProducto = await _context.salidaProductos.FindAsync(id);
            if (salidaProducto == null)
            {
                return NotFound();
            }

            _context.salidaProductos.Remove(salidaProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool salidaProductoExists(int id)
        {
            return _context.salidaProductos.Any(e => e.codigoReciboVenta == id);
        }
    }
}
