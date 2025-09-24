using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

[PrimaryKey("codigoReciboVenta", "codigoProducto")]
[Table("detallesRecibo")]
public partial class detallesRecibo
{
    [Key]
    public int codigoReciboVenta { get; set; }

    [Key]
    public int codigoProducto { get; set; }

    public double cantidadProductoRecibo { get; set; }

    public double valorUnitarioRecibo { get; set; }

    public double totalProductoRecibo { get; set; }

    [ForeignKey("codigoProducto")]
    [InverseProperty("detallesRecibos")]
    public virtual producto codigoProductoNavigation { get; set; } = null!;

    [ForeignKey("codigoReciboVenta")]
    [InverseProperty("detallesRecibos")]
    public virtual reciboVenta codigoReciboVentaNavigation { get; set; } = null!;
}
