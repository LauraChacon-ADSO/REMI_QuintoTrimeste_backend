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
    public class proveedoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public proveedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<proveedores>>> Getproveedores()
        {
            var proveedor = await _context.proveedores.ToListAsync();
            return Ok(proveedor);
        }

        // GET: api/proveedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<proveedores>> Getproveedores(string id)
        {
            var proveedores = await _context.proveedores.FindAsync(id);

            if (proveedores == null)
            {
                return NotFound();
            }

            return proveedores;
        }

        // PUT: api/proveedores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putproveedore(string id, EditarProveedorDto editarProveedorDto)
        {
            var proveedor = await _context.proveedores.FindAsync(id);
            if (proveedor == null) return NotFound();

            proveedor.tipoDocumentoProveedor = editarProveedorDto.tipoDocumentoProveedor;
            proveedor.documentoProveedor = editarProveedorDto.documentoProveedor;
            proveedor.nombreProveedor = editarProveedorDto.nombreProveedor;
            proveedor.correoProveedor = editarProveedorDto.correoProveedor;
            proveedor.telefonoProveedor = editarProveedorDto.telefonoProveedor;

            await _context.SaveChangesAsync();
            return NoContent();

        }

        // POST: api/proveedores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<proveedores>> Postproveedore(CrearProveedorDto crearProveedorDto)
        {
            var dcProveedorE = await _context.proveedores
               .AnyAsync(pv => pv.documentoProveedor == crearProveedorDto.documentoProveedor);

            if (dcProveedorE)

            {
                return BadRequest("El numero de documento ya existe");
            }

            var nuevoProveedor = new proveedores
            {
                documentoProveedor = crearProveedorDto.documentoProveedor,
                tipoDocumentoProveedor = crearProveedorDto.tipoDocumentoProveedor,
                nombreProveedor = crearProveedorDto.nombreProveedor,
                correoProveedor = crearProveedorDto.correoProveedor,
                telefonoProveedor = crearProveedorDto.telefonoProveedor,
            };

            _context.proveedores.AddAsync(nuevoProveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getproveedore", new { id = nuevoProveedor.documentoProveedor }, nuevoProveedor);
        }

        // DELETE: api/proveedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteproveedore(string id)
        {
            {
                var proveedore = await _context.proveedores
                    .Include(p => p.productos)
                    .FirstOrDefaultAsync(p => p.documentoProveedor == id);

                if (proveedore == null)
                {
                    return NotFound();
                }


                if (proveedore.productos.Any())
                {
                    return BadRequest("No se puede eliminar el proveedor porque tiene productos asociados.");
                }

                _context.proveedores.Remove(proveedore);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }
    }
}
