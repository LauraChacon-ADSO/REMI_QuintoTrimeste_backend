using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class CrearProductoDto
    {
        [StringLength(45)]
        [Unicode(false)]
        public string nombreProducto { get; set; } = null!;

        public int? entradaProducto { get; set; }

        [StringLength(45)]
        [Unicode(false)]
        public string? marcaProducto { get; set; }

        public double? precioProducto { get; set; }
        public int CodigoSubCategoria { get; set; }

        public string documentoProveedor { get; set; } = null!;
    }
}
