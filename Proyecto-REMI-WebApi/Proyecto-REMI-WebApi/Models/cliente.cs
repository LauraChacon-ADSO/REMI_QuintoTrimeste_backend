using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

[Table("cliente")]
public partial class cliente
{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string documentoCliente { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string tipoDocumentoCliente { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string nombreCliente { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string apellidoCliente { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string? correoCliente { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string? telefonoCliente { get; set; }

    [InverseProperty("documentoClienteNavigation")]
    public virtual ICollection<pedido> pedidos { get; set; } = new List<pedido>();
}
