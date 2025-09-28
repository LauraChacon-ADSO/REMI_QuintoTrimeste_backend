using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class PedidoDto
    {
        [Key]
        public int codigoPedido { get; set; }

        public DateOnly fechaPedido { get; set; }

        public TimeOnly horaPedido { get; set; }

        [StringLength(20)]
        [Unicode(false)]
        public string documentoCliente { get; set; } = null!;

        [StringLength(10)]
        [Unicode(false)]
        public string estadoPedido { get; set; } = null!;

        public List<pedidoDetalleDto> detallesP { get; set; } = new();
    }
}
