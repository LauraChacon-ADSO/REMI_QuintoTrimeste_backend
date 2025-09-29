namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class ventaReciboDto
    {
        public int codigoReciboVenta { get; set; }

        public DateTime fechaRecibo { get; set; }

        public decimal totalRecibo { get; set; }

        // 🔽 El pedido asociado a este recibo
        public PedidoDto Pedido { get; set; } = null!;
    }
}
