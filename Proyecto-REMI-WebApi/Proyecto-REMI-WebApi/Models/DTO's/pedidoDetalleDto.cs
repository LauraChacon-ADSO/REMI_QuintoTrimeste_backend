namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class pedidoDetalleDto
    {
        public int codigoProducto { get; set; }
        public int cantidadProducto { get; set; }
        public decimal valorProducto { get; set; }
        public decimal totalPagoProducto { get; set; }
    }
}
