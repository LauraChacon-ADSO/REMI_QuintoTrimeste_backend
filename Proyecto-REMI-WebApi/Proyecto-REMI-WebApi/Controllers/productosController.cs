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
    public class productosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public productosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/productoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<producto>>> Getproductos()
        {
            var productos = await _context.productos.ToListAsync();
            return Ok(productos);
        }

        // GET: api/productoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<producto>> Getproducto(int id)
        {
            var producto = await _context.productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }


        [HttpGet("subcategoria/{subCategoriaId}")]
        public async Task<ActionResult<IEnumerable<producto>>> GetProductosBySubCategoria(int subCategoriaId)
        {
            var productos = await _context.productos
                .Where(p => p.codigoSubCategorias == subCategoriaId)
                .ToListAsync();

            return Ok(productos ?? new List<producto>());
        }


        [HttpGet("buscar/{nombre?}")]
        public async Task<ActionResult<IEnumerable<producto>>> Buscar(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return Ok(await _context.productos.ToListAsync());
            }

            nombre = nombre.Trim().ToLower();

            var productos = await _context.productos
                .Where(p => p.nombreProducto.ToLower().Contains(nombre))
                .ToListAsync();

            return Ok(productos);
        }


        // PUT: api/productoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putproducto(int id, EditarProductosDto editarproductosDto)
        {
            var producto = await _context.productos.FindAsync(id);
            if (producto == null) return NotFound();


            producto.nombreProducto = editarproductosDto.nombreProducto;
            producto.marcaProducto = editarproductosDto.marcaProducto;
            producto.precioProducto=editarproductosDto.precioProducto;

            await _context.SaveChangesAsync();
            return NoContent();

        }

        // POST: api/productoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<producto>> Postproducto(CrearProductoDto dto)
        {

            var nombreExistente = await _context.productos
                .AnyAsync(p => p.nombreProducto == dto.nombreProducto);

            if (nombreExistente)
            {
                return BadRequest("El nombre del producto ya existe");
            }


            var subcategoria = await _context.subCategorias
                .FirstOrDefaultAsync(sc => sc.codigoSubCategorias == dto.CodigoSubCategoria);

            if (subcategoria == null)
            {
                return BadRequest("La subcategoría no existe. No se puede crear el producto.");
            }

            var EntradaP = new producto
            {
                nombreProducto = dto.nombreProducto,
                entradaProducto = dto.entradaProducto,
                marcaProducto = dto.marcaProducto,
                precioProducto = dto.precioProducto,
                codigoSubCategorias = dto.CodigoSubCategoria,
                documentoProveedor = dto.documentoProveedor
            };

            _context.productos.Add(EntradaP);
            await _context.SaveChangesAsync();


            var stock = new stock
            {
                codigoProducto = EntradaP.codigoProducto,
                cantidadActual = 0,
                stockMin = 5,
                stockMax = 50,
                estadoStock = 1
            };

            _context.stocks.Add(stock);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getproducto", new { id = EntradaP.codigoProducto }, EntradaP);

        }


        // DELETE: api/productoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteproducto(int id)
        {

            var producto = await _context.productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }


            _context.productos.Remove(producto);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {

                Console.WriteLine(ex.InnerException?.Message);
                return BadRequest("No se pudo eliminar el producto. Revisa las relaciones en la base de datos.");
            }

            return NoContent();

        }
    }
}