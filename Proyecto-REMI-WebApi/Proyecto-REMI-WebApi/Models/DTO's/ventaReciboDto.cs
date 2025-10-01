namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class ventaReciboDto
    {
        public int codigoReciboVenta { get; set; }
        public DateOnly fechaReciboVenta { get; set; }
        public TimeOnly horaReciboVenta { get; set; }

        // Relación con pedido
        public int codigoPedido { get; set; }
        public string documentoCliente { get; set; } = null!;
        public string estadoPedido { get; set; } = null!;

        public List<pedidoDetalleDto> detalles { get; set; } = new();

        public decimal totalVenta { get; set; }

        public decimal valorPago { get; set; }

        public decimal cambio { get; set; }

        public decimal saldoPendiente { get; set; }
    }
}
