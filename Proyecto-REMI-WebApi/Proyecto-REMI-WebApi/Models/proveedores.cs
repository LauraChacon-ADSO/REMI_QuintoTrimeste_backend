using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

public partial class proveedores
{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string documentoProveedor { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string tipoDocumentoProveedor { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string nombreProveedor { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string? correoProveedor { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string? telefonoProveedor { get; set; }

    [InverseProperty("codigoProveedorNavigation")]
    public virtual ICollection<entradaProducto> entradaProductos { get; set; } = new List<entradaProducto>();

    [InverseProperty("documentoProveedorNavigation")]
    public virtual ICollection<producto> productos { get; set; } = new List<producto>();
}
