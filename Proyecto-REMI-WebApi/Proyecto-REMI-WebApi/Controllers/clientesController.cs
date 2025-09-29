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
    public class clientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public clientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<cliente>>> GetClientes()
        {
            var clientes = await _context.clientes
                .Include(c => c.pedidos)  
                .ToListAsync();

            return Ok(clientes);
        }

        // GET: api/clientes/5
        [HttpGet("{documentoCliente}")]
        public async Task<ActionResult<cliente>> GetCliente(string documentoCliente)
        {
            var cliente = await _context.clientes
                .Include(c => c.pedidos)   
                .FirstOrDefaultAsync(c => c.documentoCliente == documentoCliente);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        // PUT: api/clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcliente(string id, cliente cliente)
        {
            if (id != cliente.documentoCliente)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clienteExists(id))
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

        // POST: api/clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<cliente>> Postcliente(cliente cliente)
        {
            _context.clientes.Add(cliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (clienteExists(cliente.documentoCliente))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getcliente", new { id = cliente.documentoCliente }, cliente);
        }

        // DELETE: api/clientes/5
        [HttpDelete("{documentoCliente}")]
        public async Task<IActionResult> Deletecliente(string documentoCliente)
        {
            var cliente = await _context.clientes.FindAsync(documentoCliente);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool clienteExists(string id)
        {
            return _context.clientes.Any(e => e.documentoCliente == id);
        }
    }
}
