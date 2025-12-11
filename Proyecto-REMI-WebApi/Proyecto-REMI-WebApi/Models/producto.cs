using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

[Table("producto")]
public partial class producto
{
    [Key]
    public int codigoProducto { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string nombreProducto { get; set; } = null!;

    public int? entradaProducto { get; set; }

    public int? salidaProducto { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string? marcaProducto { get; set; }

    public decimal precioProducto { get; set; }

    public int codigoSubCategorias { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string documentoProveedor { get; set; } = null!;

    [ForeignKey("codigoSubCategorias")]
    [InverseProperty("productos")]
    public virtual subCategoria codigoSubCategoriasNavigation { get; set; } = null!;

    [InverseProperty("codigoProductoNavigation")]
    public virtual ICollection<detallesPedido> detallesPedidos { get; set; } = new List<detallesPedido>();

    [InverseProperty("codigoProductoNavigation")]
    public virtual ICollection<detallesRecibo> detallesRecibos { get; set; } = new List<detallesRecibo>();

    [ForeignKey("documentoProveedor")]
    [InverseProperty("productos")]
    public virtual proveedores documentoProveedorNavigation { get; set; } = null!;

    [InverseProperty("codigoProductoNavigation")]
    public virtual ICollection<productoEntrada> productoEntrada { get; set; } = new List<productoEntrada>();

    [InverseProperty("codigoProductoNavigation")]
    public virtual ICollection<salidaProducto> salidaProductos { get; set; } = new List<salidaProducto>();

    [InverseProperty("codigoProductoNavigation")]
    public virtual ICollection<stock> stocks { get; set; } = new List<stock>();
}
