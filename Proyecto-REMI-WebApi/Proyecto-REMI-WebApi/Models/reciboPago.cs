using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

[PrimaryKey("codigoReciboVenta", "codigoFormaPago")]
public partial class reciboPago
{
    [Key]
    public int codigoReciboVenta { get; set; }

    [Key]
    public int codigoFormaPago { get; set; }

    public decimal valorPago { get; set; }

    [ForeignKey("codigoFormaPago")]
    [InverseProperty("reciboPagos")]
    public virtual formaPago codigoFormaPagoNavigation { get; set; } = null!;

    [ForeignKey("codigoReciboVenta")]
    [InverseProperty("reciboPagos")]
    public virtual reciboVenta codigoReciboVentaNavigation { get; set; } = null!;
}
