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
        public async Task<ActionResult<pedido>> Postpedido(pedido pedido)
        {
            _context.pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getpedido", new { id = pedido.codigoPedido }, pedido);
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
