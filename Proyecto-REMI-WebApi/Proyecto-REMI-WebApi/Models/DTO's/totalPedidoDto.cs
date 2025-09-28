namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class totalPedidoDto
    {
        public int codigoPedido { get; set; }
        public int codigoProducto { get; set; }
        public DateOnly fechaPedido { get; set; }
        public string documentoCliente { get; set; } = null!;
        public List<pedidoDetalleDto> detallesPe { get; set; } = new();
        public double valorPedido { get; set; }
    }
}
