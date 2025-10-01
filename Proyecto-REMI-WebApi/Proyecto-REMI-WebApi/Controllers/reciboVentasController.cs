using Humanizer;
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
    public class reciboVentasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public reciboVentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/reciboVentas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ventaReciboDto>>> GetRecibos()
        {
            var recibos = await _context.reciboVenta
                .Include(r => r.codigoPedidoNavigation)
                    .ThenInclude(p => p.detallesPedidos)
                .Include(r => r.reciboPagos)
                .Select(r => new ventaReciboDto
                {
                    codigoReciboVenta = r.codigoReciboVenta,
                    fechaReciboVenta = r.fechaReciboVenta,
                    horaReciboVenta = r.horaReciboVenta,
                    totalVenta = r.totalVenta,
                    valorPago = r.reciboPagos.Sum(p => p.valorPago),
                    saldoPendiente = Math.Max(0, r.totalVenta - r.reciboPagos.Sum(p => p.valorPago)),
                    codigoPedido = r.codigoPedidoNavigation.codigoPedido,
                    documentoCliente = r.codigoPedidoNavigation.documentoCliente,
                    estadoPedido = r.codigoPedidoNavigation.estadoPedido,
                    detalles = r.codigoPedidoNavigation.detallesPedidos
                        .Select(d => new pedidoDetalleDto
                        {
                            codigoProducto = d.codigoProducto,
                            cantidadProducto = d.cantidadProducto,
                            valorProducto = d.valorProducto,
                            totalPagoProducto = d.totalPagoProducto
                        }).ToList(),
                    cambio = r.reciboPagos.Sum(p => p.valorPago) > r.totalVenta
                     ? r.reciboPagos.Sum(p => p.valorPago) - r.totalVenta
                     : 0
                })
                .ToListAsync();

            return Ok(recibos);
        }

        // GET: api/reciboVenta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ventaReciboDto>> GetRecibo(int id)
        {
            var recibo = await _context.reciboVenta
                .Include(r => r.codigoPedidoNavigation)
                    .ThenInclude(p => p.detallesPedidos)
                .Include(r => r.reciboPagos)
                .Where(r => r.codigoReciboVenta == id)
                .Select(r => new ventaReciboDto
                {
                    codigoReciboVenta = r.codigoReciboVenta,
                    fechaReciboVenta = r.fechaReciboVenta,
                    horaReciboVenta = r.horaReciboVenta,
                    totalVenta = r.totalVenta,
                    valorPago = r.reciboPagos.Sum(p => p.valorPago),
                    saldoPendiente = Math.Max(0, r.totalVenta - r.reciboPagos.Sum(p => p.valorPago)),
                    codigoPedido = r.codigoPedidoNavigation.codigoPedido,
                    documentoCliente = r.codigoPedidoNavigation.documentoCliente,
                    estadoPedido = r.codigoPedidoNavigation.estadoPedido,
                    detalles = r.codigoPedidoNavigation.detallesPedidos
                        .Select(d => new pedidoDetalleDto
                        {
                            codigoProducto = d.codigoProducto,
                            cantidadProducto = d.cantidadProducto,
                            valorProducto = d.valorProducto,
                            totalPagoProducto = d.totalPagoProducto
                        }).ToList(),
                    cambio = r.reciboPagos.Sum(p => p.valorPago) > r.totalVenta
                     ? r.reciboPagos.Sum(p => p.valorPago) - r.totalVenta
                     : 0
                })
                .FirstOrDefaultAsync();

            if (recibo == null) return NotFound();
            return Ok(recibo);
        }

        // POST: api/reciboVenta
        [HttpPost]
        public async Task<ActionResult> PostReciboPago([FromBody] crearReciboPagoDto dto)
        {
            var recibo = await _context.reciboVenta
                .FirstOrDefaultAsync(r => r.codigoReciboVenta == dto.codigoReciboVenta);

            if (recibo == null)
                return NotFound("Recibo no encontrado");

            // Guardar el pago
            var pago = new reciboPago
            {
                codigoReciboVenta = dto.codigoReciboVenta,
                codigoFormaPago = dto.codigoFormaPago,
                valorPago = dto.valorPago
            };

            _context.reciboPagos.Add(pago);

            // Recalcular saldo pendiente
            var totalPagado = _context.reciboPagos
                .Where(rp => rp.codigoReciboVenta == dto.codigoReciboVenta)
                .Sum(rp => rp.valorPago) + dto.valorPago; // incluir el que estoy insertando

            recibo.saldoPendiente = Math.Max(0, recibo.totalVenta - totalPagado);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                recibo.codigoReciboVenta,
                recibo.totalVenta,
                recibo.saldoPendiente
            });
        }

        // PUT: api/reciboVenta/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecibo(int id, reciboVenta reciboUpdate)
        {
            if (id != reciboUpdate.codigoReciboVenta)
                return BadRequest();

            var recibo = await _context.reciboVenta
                .Include(r => r.reciboPagos)
                .FirstOrDefaultAsync(r => r.codigoReciboVenta == id);

            if (recibo == null)
                return NotFound();

            // Actualizar campos editables
            recibo.fechaReciboVenta = reciboUpdate.fechaReciboVenta;
            recibo.horaReciboVenta = reciboUpdate.horaReciboVenta;
            recibo.totalVenta = reciboUpdate.totalVenta;

            // Recalcular saldo
            var totalPagado = recibo.reciboPagos.Sum(p => p.valorPago);
            var saldoPendiente = recibo.totalVenta - totalPagado;

            recibo.saldoPendiente = saldoPendiente < 0 ? 0 : saldoPendiente;

            await _context.SaveChangesAsync();

            // Calcular cambio solo para respuesta
            var cambio = totalPagado > recibo.totalVenta
                         ? totalPagado - recibo.totalVenta
                         : 0;

            return Ok(new
            {
                recibo.codigoReciboVenta,
                recibo.totalVenta,
                totalPagado,
                recibo.saldoPendiente,
                cambio
            });
        }


        // DELETE: api/reciboVenta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecibo(int id)
        {
            var recibo = await _context.reciboVenta.FindAsync(id);
            if (recibo == null) return NotFound();

            _context.reciboVenta.Remove(recibo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

