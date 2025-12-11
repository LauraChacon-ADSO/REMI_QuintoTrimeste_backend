using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class CrearEntradaProductoDTO
    {
        public DateOnly fechaEntrada { get; set; }

        [StringLength(45)]
        [Unicode(false)]
        public string numeroFacturaEntrada { get; set; } = null!;

        [StringLength(20)]
        [Unicode(false)]
        public string codigoProveedor { get; set; } = null!;
    }
}
