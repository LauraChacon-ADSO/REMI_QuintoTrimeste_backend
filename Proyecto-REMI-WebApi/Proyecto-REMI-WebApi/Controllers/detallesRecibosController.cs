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
    public class detallesRecibosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public detallesRecibosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/detallesRecibos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<detallesRecibo>>> GetdetallesRecibos()
        {
            return await _context.detallesRecibos.ToListAsync();
        }

        // GET: api/detallesRecibos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<detallesRecibo>> GetdetallesRecibo(int id)
        {
            var detallesRecibo = await _context.detallesRecibos.FindAsync(id);

            if (detallesRecibo == null)
            {
                return NotFound();
            }

            return detallesRecibo;
        }

        // PUT: api/detallesRecibos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutdetallesRecibo(int id, detallesRecibo detallesRecibo)
        {
            if (id != detallesRecibo.codigoReciboVenta)
            {
                return BadRequest();
            }

            _context.Entry(detallesRecibo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!detallesReciboExists(id))
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

        // POST: api/detallesRecibos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<detallesRecibo>> PostdetallesRecibo(detallesRecibo detallesRecibo)
        {
            _context.detallesRecibos.Add(detallesRecibo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (detallesReciboExists(detallesRecibo.codigoReciboVenta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetdetallesRecibo", new { id = detallesRecibo.codigoReciboVenta }, detallesRecibo);
        }

        // DELETE: api/detallesRecibos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletedetallesRecibo(int id)
        {
            var detallesRecibo = await _context.detallesRecibos.FindAsync(id);
            if (detallesRecibo == null)
            {
                return NotFound();
            }

            _context.detallesRecibos.Remove(detallesRecibo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool detallesReciboExists(int id)
        {
            return _context.detallesRecibos.Any(e => e.codigoReciboVenta == id);
        }
    }
}
