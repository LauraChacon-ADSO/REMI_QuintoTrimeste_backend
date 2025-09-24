using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

[PrimaryKey("codigoProducto", "codigoEntrada")]
public partial class productoEntrada
{
    [Key]
    public int codigoProducto { get; set; }

    [Key]
    public int codigoEntrada { get; set; }

    public int cantidadProductoEntrada { get; set; }

    public double valorUnitarioProductoEntrada { get; set; }

    public double totalProductoEntrada { get; set; }

    [ForeignKey("codigoEntrada")]
    [InverseProperty("productoEntrada")]
    public virtual entradaProducto codigoEntradaNavigation { get; set; } = null!;

    [ForeignKey("codigoProducto")]
    [InverseProperty("productoEntrada")]
    public virtual producto codigoProductoNavigation { get; set; } = null!;
}
