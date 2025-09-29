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
                    .ThenInclude(rp => rp.codigoFormaPagoNavigation)
                .Select(r => new ventaReciboDto
                {
                    codigoReciboVenta = r.codigoReciboVenta,
                    fechaRecibo = r.fechaReciboVenta,
                    horaRecibo = r.horaReciboVenta,
                    totalRecibo = r.totalVenta,
                    Pedido = new PedidoDto
                    {
                        codigoPedido = r.codigoPedidoNavigation.codigoPedido,
                        estadoPedido = r.codigoPedidoNavigation.estadoPedido,
                        valorPedido = r.codigoPedidoNavigation.valorPedido,
                        documentoCliente = r.codigoPedidoNavigation.documentoCliente,
                        detallesP = r.codigoPedidoNavigation.detallesPedidos
                            .Select(d => new pedidoDetalleDto
                            {
                                codigoProducto = d.codigoProducto,
                                cantidadProducto = d.cantidadProducto,
                                valorProducto = d.valorProducto,
                                totalPagoProducto = d.totalPagoProducto
                            }).ToList()
                    },
                    formasPago = r.reciboPagos.Select(rp => new pagoFormaDto
                    {
                        codigoFormaPago = rp.codigoFormaPago,
                        nombreFormaPago = rp.codigoFormaPagoNavigation.nombreFormaPago, // <- navegación
                        valorPago = rp.valorPago
                    }).ToList()

                })
                .ToListAsync();

            return Ok(recibos);
        }
        // GET: api/reciboVentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ventaReciboDto>> GetRecibo(int id)
        {
            var recibo = await _context.reciboVenta
                .Include(r => r.codigoPedidoNavigation)
                    .ThenInclude(p => p.detallesPedidos)
                .Include(r => r.reciboPagos)
                    .ThenInclude(rp => rp.codigoFormaPagoNavigation)
                .Where(r => r.codigoReciboVenta == id)
                .Select(r => new ventaReciboDto
                {
                    codigoReciboVenta = r.codigoReciboVenta,
                    fechaRecibo = r.fechaReciboVenta,
                    horaRecibo = r.horaReciboVenta,
                    totalRecibo = r.totalVenta,
                    Pedido = new PedidoDto
                    {
                        codigoPedido = r.codigoPedidoNavigation.codigoPedido,
                        estadoPedido = r.codigoPedidoNavigation.estadoPedido,
                        valorPedido = r.codigoPedidoNavigation.valorPedido,
                        documentoCliente = r.codigoPedidoNavigation.documentoCliente,
                        detallesP = r.codigoPedidoNavigation.detallesPedidos.Select(d => new pedidoDetalleDto
                        {
                            codigoProducto = d.codigoProducto,
                            cantidadProducto = d.cantidadProducto,
                            valorProducto = d.valorProducto,
                            totalPagoProducto = d.totalPagoProducto
                        }).ToList()
                    },
                    formasPago = r.reciboPagos.Select(rp => new pagoFormaDto
                    {
                        codigoFormaPago = rp.codigoFormaPago,
                        nombreFormaPago = rp.codigoFormaPagoNavigation.nombreFormaPago,
                        valorPago = rp.valorPago
                    }).ToList()
                })
        .ToListAsync();

            return Ok(recibo);
        }

        // POST: api/reciboVentas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PedidoDto>> PostPedido([FromBody] PedidoDto dto)
        {
            var pedido = new pedido
            {
                estadoPedido = dto.estadoPedido,
                valorPedido = dto.valorPedido,
                documentoCliente = dto.documentoCliente,
                fechaPedido = DateOnly.FromDateTime(DateTime.Now)
            };

            _context.pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            if (pedido.estadoPedido == "Completado")
            {
                var recibo = new reciboVenta
                {
                    fechaReciboVenta = DateOnly.FromDateTime(DateTime.Now),
                    horaReciboVenta = TimeOnly.FromDateTime(DateTime.Now),
                    totalVenta = pedido.valorPedido,
                    codigoPedido = pedido.codigoPedido
                };

                _context.reciboVenta.Add(recibo);
                await _context.SaveChangesAsync();
            }

            dto.codigoPedido = pedido.codigoPedido; // devolver ID generado

            return CreatedAtAction(nameof(GetRecibos), new { id = pedido.codigoPedido }, dto);
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
