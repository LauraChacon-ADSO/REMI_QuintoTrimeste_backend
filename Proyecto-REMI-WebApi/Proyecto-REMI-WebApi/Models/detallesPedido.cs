using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

[PrimaryKey("codigoPedido", "codigoProducto")]
[Table("detallesPedido")]
public partial class detallesPedido
{

    [Key]
    public int codigoPedido { get; set; }

    [Key]
    public int codigoProducto { get; set; }

    public int cantidadProducto { get; set; }

    public decimal valorProducto { get; set; }

    public decimal totalPagoProducto { get; set; }

    [ForeignKey("codigoPedido")]
    [InverseProperty("detallesPedidos")]
    public virtual pedido codigoPedidoNavigation { get; set; } = null!;

    [ForeignKey("codigoProducto")]
    [InverseProperty("detallesPedidos")]
    public virtual producto codigoProductoNavigation { get; set; } = null!;
}
