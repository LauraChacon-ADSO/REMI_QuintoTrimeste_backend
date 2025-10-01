namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class pedidoCreateDto
    {
        public string documentoCliente { get; set; } = null!;
        public string estadoPedido { get; set; } = null!;
        public List<pedidoDetalleCreateDto> detallesP { get; set; } = new();
    }
}
