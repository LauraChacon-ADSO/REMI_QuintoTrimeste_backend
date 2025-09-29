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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> Getpedidos()
        {
            var pedidos = await _context.pedidos
                .Include(p => p.detallesPedidos) // 👈 incluye los detalles
                .Select(p => new PedidoDto
                {
                    codigoPedido = p.codigoPedido,
                    fechaPedido = p.fechaPedido,
                    horaPedido = p.horaPedido,
                    documentoCliente = p.documentoCliente,
                    estadoPedido = p.estadoPedido,
                    valorPedido = p.valorPedido, // 👈 ya calculado por el trigger
                    detallesP = p.detallesPedidos.Select(d => new pedidoDetalleDto
                    {
                        codigoProducto = d.codigoProducto,
                        cantidadProducto = d.cantidadProducto,
                        valorProducto = d.valorProducto,       // 👈 lo que puso el trigger
                        totalPagoProducto = d.totalPagoProducto // 👈 también del trigger
                    }).ToList()
                })
                .ToListAsync();

            return Ok(pedidos);
        }


        // GET: api/PedidosInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<pedido>> Getpedido(int id)
        {
            var pedido = await _context.pedidos
            .Include(p => p.detallesPedidos) // traemos también los detalles
            .Where(p => p.codigoPedido == id) // filtramos por id
            .Select(p => new PedidoDto
            {
                codigoPedido = p.codigoPedido,
                fechaPedido = p.fechaPedido,
                horaPedido = p.horaPedido,
                documentoCliente = p.documentoCliente,
                estadoPedido = p.estadoPedido,
                detallesP = p.detallesPedidos.Select(i => new Models.DTO_s.pedidoDetalleDto
                {
                    codigoProducto = i.codigoProducto,
                    cantidadProducto = i.cantidadProducto,
                    valorProducto = i.valorProducto
                }).ToList()
            })
            .FirstOrDefaultAsync();

             if (pedido == null)
             {
                return NotFound();
             }

             return Ok(pedido);
        }

        // PUT: api/PedidosInfo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{codigoPedido}")]
        public async Task<IActionResult> Putpedido(int codigoPedido, [FromBody] PedidoDto dto)
        {
            if (codigoPedido != dto.codigoPedido)
            {
                return BadRequest();
            }

            var pedido = await _context.pedidos
                .Include(p => p.detallesPedidos) // 👈 Incluimos los detalles
                .FirstOrDefaultAsync(p => p.codigoPedido == codigoPedido);

            if (pedido == null)
            {
                return NotFound();
            }

            // ✅ Actualizar campos principales
            pedido.estadoPedido = dto.estadoPedido;
            pedido.valorPedido = dto.valorPedido;
            pedido.documentoCliente = dto.documentoCliente;
            pedido.fechaPedido = dto.fechaPedido;
            pedido.horaPedido = dto.horaPedido;

            // ✅ Si necesitas actualizar detalles
            if (dto.detallesP != null && dto.detallesP.Any())
            {
                // Borro los detalles anteriores
                pedido.detallesPedidos.Clear();

                // Agrego los nuevos
                foreach (var detalleDto in dto.detallesP)
                {
                    pedido.detallesPedidos.Add(new detallesPedido
                    {
                        codigoProducto = detalleDto.codigoProducto,
                        cantidadProducto = detalleDto.cantidadProducto,
                        valorProducto = detalleDto.valorProducto,
                        totalPagoProducto = detalleDto.totalPagoProducto
                    });
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!pedidoExists(codigoPedido))
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


        // POST: api/PedidosInfo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<pedido>> Postpedido([FromBody] PedidoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = new pedido
            {
                fechaPedido = dto.fechaPedido,
                horaPedido = dto.horaPedido,
                documentoCliente = dto.documentoCliente,
                estadoPedido = dto.estadoPedido,
                detallesPedidos = dto.detallesP.Select(i => new Proyecto_REMI_WebApi.Models.detallesPedido
                {
                    codigoProducto = i.codigoProducto,
                    cantidadProducto = i.cantidadProducto
                }).ToList()
            };

            _context.pedidos.Add(order);
            await _context.SaveChangesAsync();

            // 👇 Recargar el pedido completo después de que el trigger actualizó valores
            await _context.Entry(order).ReloadAsync();
            await _context.Entry(order).Collection(p => p.detallesPedidos).LoadAsync();

            var result = new Proyecto_REMI_WebApi.Models.DTO_s.totalPedidoDto
            {
                codigoPedido = order.codigoPedido,
                fechaPedido = order.fechaPedido,
                documentoCliente = order.documentoCliente,
                valorPedido = order.valorPedido,  // 👈 ahora debería estar con valor del trigger
                detallesPe = order.detallesPedidos.Select(i => new Proyecto_REMI_WebApi.Models.DTO_s.pedidoDetalleDto
                {
                    codigoProducto = i.codigoProducto,
                    cantidadProducto = i.cantidadProducto,
                    valorProducto = i.valorProducto,
                    totalPagoProducto = i.totalPagoProducto
                }).ToList(),
            };

            return CreatedAtAction(nameof(Getpedidos), new { order.codigoPedido }, result);
        }

        // DELETE: api/PedidosInfo/5
        [HttpDelete("{codigoPedido}")]
        public async Task<IActionResult> Deletepedido(int codigoPedido)
        {
            var pedido = await _context.pedidos
            .Include(p => p.detallesPedidos)
            .FirstOrDefaultAsync(p => p.codigoPedido == codigoPedido);

            if (pedido == null)
                return NotFound();

            // eliminar los detalles primero
            _context.detallesPedidos.RemoveRange(pedido.detallesPedidos);

            // luego el pedido
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
