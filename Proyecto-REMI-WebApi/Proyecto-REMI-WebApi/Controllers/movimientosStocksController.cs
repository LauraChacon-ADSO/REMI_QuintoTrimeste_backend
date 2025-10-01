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
    public class movimientosStocksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public movimientosStocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/movimientosStocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<movimientosStock>>> GetmovimientosStocks()
        {
            var movimientos = await _context.movimientosStocks.ToListAsync();
            return Ok(movimientos);
        }

        // GET: api/movimientosStocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<movimientosStock>> GetmovimientosStock(int id)
        {
            var movimientosStock = await _context.movimientosStocks.FindAsync(id);

            if (movimientosStock == null)
            {
                return NotFound();
            }

            return movimientosStock;
        }

        // PUT: api/movimientosStocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutmovimientosStock(int id, movimientosStock movimientosStock)
        {
            if (id != movimientosStock.codigoMovimientoStock)
            {
                return BadRequest();
            }

            _context.Entry(movimientosStock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!movimientosStockExists(id))
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

        // POST: api/movimientosStocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<movimientosStock>> PostmovimientosStock(movimientosStock movimientosStock)
        {
            _context.movimientosStocks.Add(movimientosStock);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetmovimientosStock", new { id = movimientosStock.codigoMovimientoStock }, movimientosStock);
        }

        // DELETE: api/movimientosStocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletemovimientosStock(int id)
        {
            var movimientosStock = await _context.movimientosStocks.FindAsync(id);
            if (movimientosStock == null)
            {
                return NotFound();
            }

            _context.movimientosStocks.Remove(movimientosStock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool movimientosStockExists(int id)
        {
            return _context.movimientosStocks.Any(e => e.codigoMovimientoStock == id);
        }
    }
}
