using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

public partial class reciboVenta
{
    [Key]
    public int codigoReciboVenta { get; set; }

    public DateOnly fechaReciboVenta { get; set; }

    public TimeOnly horaReciboVenta { get; set; }

    public double totalVenta { get; set; }

    public int codigoPedido { get; set; }

    [ForeignKey("codigoPedido")]
    [InverseProperty("reciboVenta")]
    public virtual pedido codigoPedidoNavigation { get; set; } = null!;

    [InverseProperty("codigoReciboVentaNavigation")]
    public virtual ICollection<detallesRecibo> detallesRecibos { get; set; } = new List<detallesRecibo>();

    [InverseProperty("codigoReciboVentaNavigation")]
    public virtual ICollection<movimientosStock> movimientosStocks { get; set; } = new List<movimientosStock>();

    [InverseProperty("codigoReciboVentaNavigation")]
    public virtual ICollection<reciboPago> reciboPagos { get; set; } = new List<reciboPago>();

    [InverseProperty("codigoReciboVentaNavigation")]
    public virtual ICollection<salidaProducto> salidaProductos { get; set; } = new List<salidaProducto>();
}
