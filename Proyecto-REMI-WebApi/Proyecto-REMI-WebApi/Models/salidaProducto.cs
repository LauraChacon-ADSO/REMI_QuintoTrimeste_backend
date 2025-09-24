using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

[PrimaryKey("codigoReciboVenta", "codigoProducto")]
[Table("salidaProducto")]
public partial class salidaProducto
{
    [Key]
    public int codigoReciboVenta { get; set; }

    [Key]
    public int codigoProducto { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string cantidadSalidaProducto { get; set; } = null!;

    [ForeignKey("codigoProducto")]
    [InverseProperty("salidaProductos")]
    public virtual producto codigoProductoNavigation { get; set; } = null!;

    [ForeignKey("codigoReciboVenta")]
    [InverseProperty("salidaProductos")]
    public virtual reciboVenta codigoReciboVentaNavigation { get; set; } = null!;
}
