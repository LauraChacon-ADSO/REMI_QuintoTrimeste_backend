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
    public class detallesPedidosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public detallesPedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/detallesPedidoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<detallesPedido>>> GetdetallesPedidos()
        {
            return await _context.detallesPedidos.ToListAsync();
        }

        // GET: api/detallesPedidoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<detallesPedido>> GetdetallesPedido(int id)
        {
            var detallesPedido = await _context.detallesPedidos.FindAsync(id);

            if (detallesPedido == null)
            {
                return NotFound();
            }

            return detallesPedido;
        }

        // PUT: api/detallesPedidoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutdetallesPedido(int id, detallesPedido detallesPedido)
        {
            if (id != detallesPedido.codigoPedido)
            {
                return BadRequest();
            }

            _context.Entry(detallesPedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!detallesPedidoExists(id))
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

        // POST: api/detallesPedidoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<detallesPedido>> PostdetallesPedido(detallesPedido detallesPedido)
        {
            _context.detallesPedidos.Add(detallesPedido);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (detallesPedidoExists(detallesPedido.codigoPedido))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetdetallesPedido", new { id = detallesPedido.codigoPedido }, detallesPedido);
        }

        // DELETE: api/detallesPedidoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletedetallesPedido(int id)
        {
            var detallesPedido = await _context.detallesPedidos.FindAsync(id);
            if (detallesPedido == null)
            {
                return NotFound();
            }

            _context.detallesPedidos.Remove(detallesPedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool detallesPedidoExists(int id)
        {
            return _context.detallesPedidos.Any(e => e.codigoPedido == id);
        }
    }
}
