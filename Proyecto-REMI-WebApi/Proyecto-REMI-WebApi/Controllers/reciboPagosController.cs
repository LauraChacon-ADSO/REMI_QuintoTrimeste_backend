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
    public class reciboPagosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public reciboPagosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/reciboPagos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<pagoReciboDto>>> GetReciboPagos()
        {
            var pagos = await _context.reciboPagos
                .Include(rp => rp.codigoFormaPagoNavigation)
                .Select(rp => new pagoReciboDto
                {
                    codigoReciboVenta = rp.codigoReciboVenta,
                    codigoFormaPago = rp.codigoFormaPago,
                    nombreFormaPago = rp.codigoFormaPagoNavigation.nombreFormaPago,
                    valorPago = rp.valorPago
                })
                .ToListAsync();

            return Ok(pagos);
        }

        // GET: api/reciboPago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<pagoReciboDto>> GetReciboPago(int id)
        {
            var pago = await _context.reciboPagos
                .Include(rp => rp.codigoFormaPagoNavigation)
                .Where(rp => rp.codigoReciboVenta == id)
                .Select(rp => new pagoReciboDto
                {
                    codigoReciboVenta = rp.codigoReciboVenta,
                    codigoFormaPago = rp.codigoFormaPago,
                    nombreFormaPago = rp.codigoFormaPagoNavigation.nombreFormaPago,
                    valorPago = rp.valorPago
                })
                .FirstOrDefaultAsync();

            if (pago == null) return NotFound();
            return Ok(pago);
        }

        // POST: api/reciboPago
        [HttpPost]
        public async Task<ActionResult<crearReciboPagoDto>> PostReciboPago([FromBody] crearReciboPagoDto dto)
        {
            var recibo = await _context.reciboVenta.FindAsync(dto.codigoReciboVenta);
            if (recibo == null) return NotFound("Recibo no encontrado");

            var reciboPago = new reciboPago
            {
                codigoReciboVenta = dto.codigoReciboVenta,
                codigoFormaPago = dto.codigoFormaPago,
                valorPago = dto.valorPago
            };

            _context.reciboPagos.Add(reciboPago);

            recibo.saldoPendiente -= dto.valorPago;
            if (recibo.saldoPendiente < 0) recibo.saldoPendiente = 0;

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReciboPago), new { id = reciboPago.codigoReciboVenta }, dto);
        }

        // PUT: api/reciboPago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReciboPago(int id, [FromBody] crearReciboPagoDto dto)
        {
            var pago = await _context.reciboPagos.FindAsync(id);
            if (pago == null) return NotFound();

            pago.codigoFormaPago = dto.codigoFormaPago;
            pago.valorPago = dto.valorPago;

            _context.Entry(pago).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/reciboPago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReciboPago(int id)
        {
            var pago = await _context.reciboPagos.FindAsync(id);
            if (pago == null) return NotFound();

            _context.reciboPagos.Remove(pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool reciboPagoExists(int id)
        {
            return _context.reciboPagos.Any(e => e.codigoReciboVenta == id);
        }
    }
}
