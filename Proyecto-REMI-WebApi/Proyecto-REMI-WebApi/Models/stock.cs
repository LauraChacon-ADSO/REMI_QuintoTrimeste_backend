using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

[Table("stock")]
public partial class stock
{
    [Key]
    public int codigoStock { get; set; }

    public int stockMin { get; set; }

    public int stockMax { get; set; }

    public int cantidadActual { get; set; }

    public byte estadoStock { get; set; }

    public int codigoProducto { get; set; }

    [ForeignKey("codigoProducto")]
    [InverseProperty("stocks")]
    public virtual producto codigoProductoNavigation { get; set; } = null!;

    [InverseProperty("codigoStockNavigation")]
    public virtual ICollection<movimientosStock> movimientosStocks { get; set; } = new List<movimientosStock>();
}
