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
    public class entradaProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public entradaProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/entradaProductoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<entradaProducto>>> GetentradaProductos()
        {

            var EntradaProducto = await _context.entradaProductos.ToListAsync();
            return Ok(EntradaProducto);
        }

        // GET: api/entradaProductoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<entradaProducto>> GetentradaProducto(int id)
        {
            var entradaProducto = await _context.entradaProductos.FindAsync(id);

            if (entradaProducto == null)
            {
                return NotFound();
            }

            return entradaProducto;
        }

        // PUT: api/entradaProductoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutentradaProducto(int id, [FromBody] EditarEntradaProductoDto editarEntradaProductoDto)
        {
            var entradaProducto = await _context.entradaProductos.FindAsync(id);
            if (entradaProducto == null) return NotFound();

            entradaProducto.fechaEntrada = editarEntradaProductoDto.fechaEntrada;
            entradaProducto.numeroFacturaEntrada = editarEntradaProductoDto.numeroFacturaEntrada;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/entradaProductoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<entradaProducto>> PostentradaProducto(CrearEntradaProductoDTO crearEntradaProductoDTO)
        {

            var NfacturaExistente = await _context.entradaProductos
            .AnyAsync(ep => ep.numeroFacturaEntrada == crearEntradaProductoDTO.numeroFacturaEntrada);

            if (NfacturaExistente)
            {
                return BadRequest("El numero de factura ya fue registrado");
            }


            var entradaProducto = new entradaProducto
            {

                fechaEntrada = crearEntradaProductoDTO.fechaEntrada,
                numeroFacturaEntrada = crearEntradaProductoDTO.numeroFacturaEntrada,
                codigoProveedor  = crearEntradaProductoDTO.codigoProveedor


            };

            _context.entradaProductos.Add(entradaProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetentradaProducto", new { id = entradaProducto.codigoEntrada }, entradaProducto);
        }


        // DELETE: api/entradaProductoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteentradaProducto(int id)
        {
            var entradaProducto = await _context.entradaProductos.FindAsync(id);
            if (entradaProducto == null)
            {
                return NotFound();
            }

            _context.entradaProductos.Remove(entradaProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
