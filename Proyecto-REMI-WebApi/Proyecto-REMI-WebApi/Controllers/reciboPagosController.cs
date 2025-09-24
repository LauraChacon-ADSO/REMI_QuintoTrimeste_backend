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
    public class reciboPagosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public reciboPagosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/reciboPagos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<reciboPago>>> GetreciboPagos()
        {
            return await _context.reciboPagos.ToListAsync();
        }

        // GET: api/reciboPagos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<reciboPago>> GetreciboPago(int id)
        {
            var reciboPago = await _context.reciboPagos.FindAsync(id);

            if (reciboPago == null)
            {
                return NotFound();
            }

            return reciboPago;
        }

        // PUT: api/reciboPagos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutreciboPago(int id, reciboPago reciboPago)
        {
            if (id != reciboPago.codigoReciboVenta)
            {
                return BadRequest();
            }

            _context.Entry(reciboPago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!reciboPagoExists(id))
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

        // POST: api/reciboPagos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<reciboPago>> PostreciboPago(reciboPago reciboPago)
        {
            _context.reciboPagos.Add(reciboPago);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (reciboPagoExists(reciboPago.codigoReciboVenta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetreciboPago", new { id = reciboPago.codigoReciboVenta }, reciboPago);
        }

        // DELETE: api/reciboPagos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletereciboPago(int id)
        {
            var reciboPago = await _context.reciboPagos.FindAsync(id);
            if (reciboPago == null)
            {
                return NotFound();
            }

            _context.reciboPagos.Remove(reciboPago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool reciboPagoExists(int id)
        {
            return _context.reciboPagos.Any(e => e.codigoReciboVenta == id);
        }
    }
}
