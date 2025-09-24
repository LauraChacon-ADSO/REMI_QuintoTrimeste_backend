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
    public class reciboVentasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public reciboVentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/reciboVentas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<reciboVenta>>> GetreciboVenta()
        {
            return await _context.reciboVenta.ToListAsync();
        }

        // GET: api/reciboVentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<reciboVenta>> GetreciboVenta(int id)
        {
            var reciboVenta = await _context.reciboVenta.FindAsync(id);

            if (reciboVenta == null)
            {
                return NotFound();
            }

            return reciboVenta;
        }

        // PUT: api/reciboVentas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutreciboVenta(int id, reciboVenta reciboVenta)
        {
            if (id != reciboVenta.codigoReciboVenta)
            {
                return BadRequest();
            }

            _context.Entry(reciboVenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!reciboVentaExists(id))
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

        // POST: api/reciboVentas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<reciboVenta>> PostreciboVenta(reciboVenta reciboVenta)
        {
            _context.reciboVenta.Add(reciboVenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetreciboVenta", new { id = reciboVenta.codigoReciboVenta }, reciboVenta);
        }

        // DELETE: api/reciboVentas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletereciboVenta(int id)
        {
            var reciboVenta = await _context.reciboVenta.FindAsync(id);
            if (reciboVenta == null)
            {
                return NotFound();
            }

            _context.reciboVenta.Remove(reciboVenta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool reciboVentaExists(int id)
        {
            return _context.reciboVenta.Any(e => e.codigoReciboVenta == id);
        }
    }
}
