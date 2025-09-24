using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

[Table("formaPago")]
public partial class formaPago
{
    [Key]
    public int codigoFormaPago { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string nombreFormaPago { get; set; } = null!;

    [InverseProperty("codigoFormaPagoNavigation")]
    public virtual ICollection<reciboPago> reciboPagos { get; set; } = new List<reciboPago>();
}
