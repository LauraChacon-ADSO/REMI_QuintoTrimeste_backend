using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_REMI_WebApi.Models.DTO_s
{
        public class EditarProductosDto
        {


            [StringLength(45)]
            [Unicode(false)]
            public string nombreProducto { get; set; } = null!;

            [StringLength(45)]
            [Unicode(false)]
            public string? marcaProducto { get; set; }

            public double? precioProducto { get; set; }

        }
    }

