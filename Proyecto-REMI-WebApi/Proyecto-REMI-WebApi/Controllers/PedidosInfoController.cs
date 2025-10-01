using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class PedidosInfoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PedidosInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PedidosInfo
        // 🔹 Helper de redondeo
        private decimal Redondear(decimal valor, int multiplo)
        {
            return Math.Round(valor / multiplo, MidpointRounding.AwayFromZero) * multiplo;
        }

        // ✅ GET: api/PedidosInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> GetPedidos()
        {
            var pedidos = await _context.pedidos
                .Include(p => p.detallesPedidos)
                .Select(p => new PedidoDto
                {
                    codigoPedido = p.codigoPedido,
                    fechaPedido = p.fechaPedido,
                    horaPedido = p.horaPedido,
                    documentoCliente = p.documentoCliente,
                    estadoPedido = p.estadoPedido,
                    valorPedido = p.valorPedido,
                    detallesP = p.detallesPedidos.Select(d => new pedidoDetalleDto
                    {
                        codigoProducto = d.codigoProducto,
                        cantidadProducto = d.cantidadProducto,
                        valorProducto = d.valorProducto,
                        totalPagoProducto = d.totalPagoProducto
                    }).ToList()
                })
                .ToListAsync();

            return Ok(pedidos);
        }

        // ✅ GET: api/PedidosInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDto>> GetPedido(int id)
        {
            var pedido = await _context.pedidos
                .Include(p => p.detallesPedidos)
                .Where(p => p.codigoPedido == id)
                .Select(p => new PedidoDto
                {
                    codigoPedido = p.codigoPedido,
                    fechaPedido = p.fechaPedido,
                    horaPedido = p.horaPedido,
                    documentoCliente = p.documentoCliente,
                    estadoPedido = p.estadoPedido,
                    valorPedido = p.valorPedido,
                    detallesP = p.detallesPedidos.Select(d => new pedidoDetalleDto
                    {
                        codigoProducto = d.codigoProducto,
                        cantidadProducto = d.cantidadProducto,
                        valorProducto = d.valorProducto,
                        totalPagoProducto = d.totalPagoProducto
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        // ✅ POST: api/PedidosInfo
        [HttpPost]
        public async Task<ActionResult<PedidoDto>> PostPedido([FromBody] pedidoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pedido = new pedido
            {
                fechaPedido = DateOnly.FromDateTime(DateTime.Now),
                horaPedido = TimeOnly.FromDateTime(DateTime.Now),
                documentoCliente = dto.documentoCliente,
                estadoPedido = dto.estadoPedido,
                detallesPedidos = new List<detallesPedido>()
            };

            // 🔄 Crear detalles con cálculo
            foreach (var d in dto.detallesP)
            {
                var valorProducto = await _context.productos
                    .Where(p => p.codigoProducto == d.codigoProducto)
                    .Select(p => p.precioProducto)
                    .FirstOrDefaultAsync();

                // Fórmula: (valorProducto / 12) * cantidadProducto
                var bruto = (valorProducto / 12m) * d.cantidadProducto;
                var totalPago = Redondear(bruto, 50); // 🔄 redondeo a múltiplo de 50

                var detalle = new detallesPedido
                {
                    codigoProducto = d.codigoProducto,
                    cantidadProducto = d.cantidadProducto,
                    valorProducto = valorProducto,
                    totalPagoProducto = totalPago
                };

                pedido.detallesPedidos.Add(detalle);
            }

            // 🔄 Calcular total pedido
            pedido.valorPedido = pedido.detallesPedidos.Sum(dp => dp.totalPagoProducto);

            _context.pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            var result = new PedidoDto
            {
                codigoPedido = pedido.codigoPedido,
                fechaPedido = pedido.fechaPedido,
                horaPedido = pedido.horaPedido,
                documentoCliente = pedido.documentoCliente,
                estadoPedido = pedido.estadoPedido,
                valorPedido = pedido.valorPedido,
                detallesP = pedido.detallesPedidos.Select(d => new pedidoDetalleDto
                {
                    codigoProducto = d.codigoProducto,
                    cantidadProducto = d.cantidadProducto,
                    valorProducto = d.valorProducto,
                    totalPagoProducto = d.totalPagoProducto
                }).ToList()
            };

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.codigoPedido }, result);
        }

        // ✅ PUT: api/PedidosInfo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, pedidoCreateDto dto)
        {
            var pedido = await _context.pedidos
                .Include(p => p.detallesPedidos)
                .FirstOrDefaultAsync(p => p.codigoPedido == id);

            if (pedido == null)
                return NotFound();

            // 🔄 Actualizar cabecera
            pedido.documentoCliente = dto.documentoCliente;
            pedido.estadoPedido = dto.estadoPedido;

            // 🔄 Reemplazar detalles
            _context.detallesPedidos.RemoveRange(pedido.detallesPedidos);
            pedido.detallesPedidos = new List<detallesPedido>();

            foreach (var d in dto.detallesP)
            {
                var valorProducto = await _context.productos
                    .Where(p => p.codigoProducto == d.codigoProducto)
                    .Select(p => p.precioProducto)
                    .FirstOrDefaultAsync();

                var bruto = (valorProducto / 12m) * d.cantidadProducto;
                var totalPago = Redondear(bruto, 50);

                var detalle = new detallesPedido
                {
                    codigoProducto = d.codigoProducto,
                    cantidadProducto = d.cantidadProducto,
                    valorProducto = valorProducto,
                    totalPagoProducto = totalPago
                };

                pedido.detallesPedidos.Add(detalle);
            }

            // 🔄 Recalcular total
            pedido.valorPedido = pedido.detallesPedidos.Sum(dp => dp.totalPagoProducto);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ✅ DELETE: api/PedidosInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.pedidos
                .Include(p => p.detallesPedidos)
                .FirstOrDefaultAsync(p => p.codigoPedido == id);

            if (pedido == null)
                return NotFound();

            _context.detallesPedidos.RemoveRange(pedido.detallesPedidos);
            _context.pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool pedidoExists(int codigoPedido)
        {
            return _context.pedidos.Any(e => e.codigoPedido == codigoPedido);
        }
    }
}
