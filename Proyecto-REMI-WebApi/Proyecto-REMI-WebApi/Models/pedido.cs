using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

public partial class pedido
{
    [Key]
    public int codigoPedido { get; set; }

    public DateOnly fechaPedido { get; set; }

    public TimeOnly horaPedido { get; set; }

    public decimal valorPedido { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string documentoCliente { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string estadoPedido { get; set; } = null!;

    [InverseProperty("codigoPedidoNavigation")]
    public virtual ICollection<detallesPedido> detallesPedidos { get; set; } = new List<detallesPedido>();

    [ForeignKey("documentoCliente")]
    [InverseProperty("pedidos")]
    public virtual cliente documentoClienteNavigation { get; set; } = null!;

    [InverseProperty("codigoPedidoNavigation")]
    public virtual ICollection<reciboVenta> reciboVenta { get; set; } = new List<reciboVenta>();
}
