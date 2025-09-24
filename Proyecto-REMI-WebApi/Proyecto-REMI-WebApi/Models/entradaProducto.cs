using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

[Table("entradaProducto")]
public partial class entradaProducto
{
    [Key]
    public int codigoEntrada { get; set; }

    public DateOnly fechaEntrada { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string numeroFacturaEntrada { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string codigoProveedor { get; set; } = null!;

    [ForeignKey("codigoProveedor")]
    [InverseProperty("entradaProductos")]
    public virtual proveedores codigoProveedorNavigation { get; set; } = null!;

    [InverseProperty("codigoEntradaNavigation")]
    public virtual ICollection<movimientosStock> movimientosStocks { get; set; } = new List<movimientosStock>();

    [InverseProperty("codigoEntradaNavigation")]
    public virtual ICollection<productoEntrada> productoEntrada { get; set; } = new List<productoEntrada>();
}
