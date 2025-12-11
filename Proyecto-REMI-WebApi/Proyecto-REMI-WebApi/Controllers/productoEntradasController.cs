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
    public class productoEntradasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public productoEntradasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/productoEntradas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<productoEntrada>>> GetproductoEntrada()
        {
            var productoEntrada = await _context.productoEntrada.ToListAsync();
            return Ok(productoEntrada);
        }

        // GET: api/productoEntradas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<productoEntrada>> GetproductoEntrada(int id)
        {
            var productoEntrada = await _context.productoEntrada.FindAsync(id);

            if (productoEntrada == null)
            {
                return NotFound();
            }

            return productoEntrada;
        }

        // PUT: api/productoEntradas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutproductoEntrada(int id, productoEntrada productoEntrada)
        {
            if (id != productoEntrada.codigoProducto)
            {
                return BadRequest();
            }

            _context.Entry(productoEntrada).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productoEntradaExists(id))
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

        // POST: api/productoEntradas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<productoEntrada>> PostproductoEntrada(CrearProductoEntradaDto crearProductoEntradaDto)
        {
            var total = crearProductoEntradaDto.cantidadProductoEntrada *
               crearProductoEntradaDto.valorUnitarioProductoEntrada;

            var productoEntrada = new productoEntrada
            {

                codigoProducto = crearProductoEntradaDto.codigoProducto,
                codigoEntrada = crearProductoEntradaDto.codigoEntrada,
                cantidadProductoEntrada = crearProductoEntradaDto.cantidadProductoEntrada,
                valorUnitarioProductoEntrada = crearProductoEntradaDto.valorUnitarioProductoEntrada,
                totalProductoEntrada = total

            };

            _context.productoEntrada.Add(productoEntrada);
            await _context.SaveChangesAsync();

            var stock = await _context.stocks
            .FirstOrDefaultAsync(s => s.codigoProducto == crearProductoEntradaDto.codigoProducto);

            if (stock != null)
            {
                stock.cantidadActual += crearProductoEntradaDto.cantidadProductoEntrada;
                _context.stocks.Update(stock);
                await _context.SaveChangesAsync();
            }
            else
            {
                stock = new stock
                {
                    codigoProducto = crearProductoEntradaDto.codigoProducto,
                    cantidadActual = crearProductoEntradaDto.cantidadProductoEntrada
                };
                _context.stocks.Add(stock);
                await _context.SaveChangesAsync();
            }

            var movimiento = new movimientosStock
            {
                codigoStock = stock.codigoStock,
                tipoMovimientoStock = "Entrada",
                fechaMovimientoStock = DateTime.Now,
                referenciaMovimientoStock = $"Entrada {crearProductoEntradaDto.codigoEntrada}",
                codigoEntrada = crearProductoEntradaDto.codigoEntrada
            };

            _context.movimientosStocks.Add(movimiento);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetproductoEntrada", new { id = productoEntrada.codigoEntrada }, productoEntrada);
        }

        // DELETE: api/productoEntradas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteproductoEntrada(int id)
        {
            var productoEntrada = await _context.productoEntrada.FindAsync(id);
            if (productoEntrada == null)
            {
                return NotFound();
            }

            _context.productoEntrada.Remove(productoEntrada);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool productoEntradaExists(int id)
        {
            return _context.productoEntrada.Any(e => e.codigoProducto == id);
        }
    }
}

