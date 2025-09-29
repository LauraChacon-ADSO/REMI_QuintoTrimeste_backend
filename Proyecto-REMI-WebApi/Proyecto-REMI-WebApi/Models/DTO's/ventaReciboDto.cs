namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class ventaReciboDto
    {
        public int codigoReciboVenta { get; set; }

        public DateOnly fechaRecibo { get; set; }
        public TimeOnly horaRecibo { get; set; }

        public double totalRecibo { get; set; }

        // 🔽 El pedido asociado a este recibo
        public PedidoDto Pedido { get; set; } = null!;
        public List<pagoFormaDto> formasPago { get; set; } = new();
    }
}
