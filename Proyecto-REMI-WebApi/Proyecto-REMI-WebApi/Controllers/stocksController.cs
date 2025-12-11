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
    public class stocksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public stocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/stocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<stock>>> Getstocks()
        {
            var stock = await _context.stocks.ToListAsync();
            return Ok(stock);
        }

        // GET: api/stocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<stock>> Getstock(int id)
        {
            var stock = await _context.stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }

        // PUT: api/stocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putstock(int id, stock stock)
        {
            if (id != stock.codigoStock)
            {
                return BadRequest();
            }

            _context.Entry(stock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!stockExists(id))
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

        // POST: api/stocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<stock>> Poststock(stock stock)
        {
            _context.stocks.Add(stock);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getstock", new { id = stock.codigoStock }, stock);
        }

        // DELETE: api/stocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletestock(int id)
        {
            var stock = await _context.stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            _context.stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool stockExists(int id)
        {
            return _context.stocks.Any(e => e.codigoStock == id);
        }
    }
}
