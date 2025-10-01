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

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult<cliente>> PostCliente([FromBody] clientesInfoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cliente = new cliente
            {
                documentoCliente = dto.documentoCliente,
                tipoDocumentoCliente = dto.tipoDocumentoCliente,
                nombreCliente = dto.nombreCliente,
                apellidoCliente = dto.apellidoCliente,
                correoCliente = dto.correoCliente,
                telefonoCliente = dto.telefonoCliente
            };

            _context.clientes.Add(cliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClienteExists(cliente.documentoCliente))
                    return Conflict();
                else
                    throw;
            }

            return CreatedAtAction(nameof(GetCliente), new { documentoCliente = cliente.documentoCliente }, cliente);
        }

        // PUT: api/clientes/5
        [HttpPut("{documentoCliente}")]
        public async Task<IActionResult> PutCliente(string documentoCliente, [FromBody] clientesInfoDto dto)
        {
            if (documentoCliente != dto.documentoCliente)
                return BadRequest("El documento de cliente no coincide.");

            var cliente = await _context.clientes.FindAsync(documentoCliente);
            if (cliente == null)
                return NotFound();

            // Mapear DTO a entidad
            cliente.tipoDocumentoCliente = dto.tipoDocumentoCliente;
            cliente.nombreCliente = dto.nombreCliente;
            cliente.apellidoCliente = dto.apellidoCliente;
            cliente.correoCliente = dto.correoCliente;
            cliente.telefonoCliente = dto.telefonoCliente;

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(documentoCliente))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/clientes/5
        [HttpDelete("{documentoCliente}")]
        public async Task<IActionResult> DeleteCliente(string documentoCliente)
        {
            var cliente = await _context.clientes.FindAsync(documentoCliente);
            if (cliente == null)
                return NotFound();

            _context.clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(string documentoCliente)
        {
            return _context.clientes.Any(e => e.documentoCliente == documentoCliente);
        }
    }
}
