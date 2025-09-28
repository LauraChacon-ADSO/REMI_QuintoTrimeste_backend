using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_REMI_WebApi.Datos;
using Proyecto_REMI_WebApi.Models;
using Proyecto_REMI_WebApi.Models.DTO_s;

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
        public async Task<ActionResult<IEnumerable<pedido>>> Getpedidos()
        {
            return await _context.pedidos.ToListAsync();
        }

        // GET: api/PedidosInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<pedido>> Getpedido(int id)
        {
            var pedido = await _context.pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // PUT: api/PedidosInfo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpedido(int id, pedido pedido)
        {
            if (id != pedido.codigoPedido)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!pedidoExists(id))
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
            // 👇 Validar que el modelo sea válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lastOrder = await _context.pedidos
                .OrderByDescending(o => o.codigoPedido)
                .FirstOrDefaultAsync();

            int nextNumber = lastOrder != null ? lastOrder.codigoPedido + 1 : 1;
            string orderNumber = $"ORD-{nextNumber:D4}";

            var order = new pedido
            {
                fechaPedido = dto.fechaPedido,
                horaPedido = dto.horaPedido,
                documentoCliente = dto.documentoCliente,
                estadoPedido = dto.estadoPedido, // 👈 no olvides este campo
                detallesPedidos = dto.detallesP.Select(i => new detallesPedido
                {
                    codigoProducto = i.codigoProducto, // 👈 aquí ojo: antes estabas copiando codigoPedido
                    cantidadProducto = i.cantidadProducto,
                    valorProducto = i.valorProducto
                }).ToList()
            };

            _context.pedidos.Add(order);
            await _context.SaveChangesAsync();

            // Mapear a DTO para devolver respuesta
            var result = new Proyecto_REMI_WebApi.Models.DTO_s.totalPedidoDto
            {
                codigoPedido = order.codigoPedido,
                fechaPedido = order.fechaPedido,
                documentoCliente = order.documentoCliente,
                detallesPe = order.detallesPedidos.Select(i => new Proyecto_REMI_WebApi.Models.DTO_s.pedidoDetalleDto
                {
                    codigoProducto = i.codigoProducto,
                    cantidadProducto = i.cantidadProducto,
                    valorProducto = i.valorProducto,
                    totalPagoProducto = Math.Floor((i.valorProducto / 12) * i.cantidadProducto)
                }).ToList(),
                valorPedido = Math.Floor(order.detallesPedidos.Sum(i => (i.valorProducto / 12) * i.cantidadProducto))
            };

            return CreatedAtAction(nameof(Getpedidos), new { order.codigoPedido }, result);
        }


        // DELETE: api/PedidosInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepedido(int id)
        {
            var pedido = await _context.pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool pedidoExists(int id)
        {
            return _context.pedidos.Any(e => e.codigoPedido == id);
        }
    }
}
