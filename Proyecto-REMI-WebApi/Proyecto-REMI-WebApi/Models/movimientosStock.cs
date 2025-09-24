using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

[Table("movimientosStock")]
public partial class movimientosStock
{
    [Key]
    public int codigoMovimientoStock { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string tipoMovimientoStock { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime fechaMovimientoStock { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string? referenciaMovimientoStock { get; set; }

    public int codigoStock { get; set; }

    public int? codigoReciboVenta { get; set; }

    public int? codigoEntrada { get; set; }

    [ForeignKey("codigoEntrada")]
    [InverseProperty("movimientosStocks")]
    public virtual entradaProducto? codigoEntradaNavigation { get; set; }

    [ForeignKey("codigoReciboVenta")]
    [InverseProperty("movimientosStocks")]
    public virtual reciboVenta? codigoReciboVentaNavigation { get; set; }

    [ForeignKey("codigoStock")]
    [InverseProperty("movimientosStocks")]
    public virtual stock codigoStockNavigation { get; set; } = null!;
}
