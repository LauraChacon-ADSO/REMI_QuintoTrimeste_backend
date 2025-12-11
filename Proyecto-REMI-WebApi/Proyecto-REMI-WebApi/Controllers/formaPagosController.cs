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
    public class formaPagosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public formaPagosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<pagoFormaDto>>> GetFormasPago()
        {
            var formasPago = await _context.formaPagos
                .Select(f => new pagoFormaDto
                {
                    codigoFormaPago = f.codigoFormaPago,
                    nombreFormaPago = f.nombreFormaPago
                })
                .ToListAsync();

            return Ok(formasPago);
        }

        // ✅ GET: api/FormaPago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<pagoFormaDto>> GetFormaPago(int id)
        {
            var formaPago = await _context.formaPagos.FindAsync(id);

            if (formaPago == null)
                return NotFound();

            return new pagoFormaDto
            {
                codigoFormaPago = formaPago.codigoFormaPago,
                nombreFormaPago = formaPago.nombreFormaPago
            };
        }

        // ✅ POST: api/FormaPago
        [HttpPost]
        public async Task<ActionResult<pagoFormaDto>> PostFormaPago([FromBody] pagoFormaDto dto)
        {
            var formaPago = new formaPago
            {
                nombreFormaPago = dto.nombreFormaPago
            };

            _context.formaPagos.Add(formaPago);
            await _context.SaveChangesAsync();

            dto.codigoFormaPago = formaPago.codigoFormaPago;

            return CreatedAtAction(nameof(GetFormaPago), new { id = dto.codigoFormaPago }, dto);
        }

        // ✅ PUT: api/FormaPago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormaPago(int id, [FromBody] pagoFormaDto dto)
        {
            if (id != dto.codigoFormaPago)
                return BadRequest();

            var formaPago = await _context.formaPagos.FindAsync(id);
            if (formaPago == null)
                return NotFound();

            formaPago.nombreFormaPago = dto.nombreFormaPago;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ DELETE: api/FormaPago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormaPago(int id)
        {
            var formaPago = await _context.formaPagos.FindAsync(id);
            if (formaPago == null)
                return NotFound();

            _context.formaPagos.Remove(formaPago);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool formaPagoExists(int id)
        {
            return _context.formaPagos.Any(e => e.codigoFormaPago == id);
        }
    }
}
