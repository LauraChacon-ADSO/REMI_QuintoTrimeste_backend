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
    public class formaPagosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public formaPagosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/formaPagoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<formaPago>>> GetformaPagos()
        {
            return await _context.formaPagos.ToListAsync();
        }

        // GET: api/formaPagoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<formaPago>> GetformaPago(int id)
        {
            var formaPago = await _context.formaPagos.FindAsync(id);

            if (formaPago == null)
            {
                return NotFound();
            }

            return formaPago;
        }

        // PUT: api/formaPagoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutformaPago(int id, formaPago formaPago)
        {
            if (id != formaPago.codigoFormaPago)
            {
                return BadRequest();
            }

            _context.Entry(formaPago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!formaPagoExists(id))
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

        // POST: api/formaPagoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<formaPago>> PostformaPago(formaPago formaPago)
        {
            _context.formaPagos.Add(formaPago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetformaPago", new { id = formaPago.codigoFormaPago }, formaPago);
        }

        // DELETE: api/formaPagoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteformaPago(int id)
        {
            var formaPago = await _context.formaPagos.FindAsync(id);
            if (formaPago == null)
            {
                return NotFound();
            }

            _context.formaPagos.Remove(formaPago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool formaPagoExists(int id)
        {
            return _context.formaPagos.Any(e => e.codigoFormaPago == id);
        }
    }
}
