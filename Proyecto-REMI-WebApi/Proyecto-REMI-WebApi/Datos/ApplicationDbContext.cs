using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Proyecto_REMI_WebApi.Models;

namespace Proyecto_REMI_WebApi.Datos;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<categoria> categorias { get; set; }

    public virtual DbSet<cliente> clientes { get; set; }

    public virtual DbSet<detallesPedido> detallesPedidos { get; set; }

    public virtual DbSet<detallesRecibo> detallesRecibos { get; set; }

    public virtual DbSet<entradaProducto> entradaProductos { get; set; }

    public virtual DbSet<formaPago> formaPagos { get; set; }

    public virtual DbSet<movimientosStock> movimientosStocks { get; set; }

    public virtual DbSet<niveles> niveles { get; set; }

    public virtual DbSet<pedido> pedidos { get; set; }

    public virtual DbSet<producto> productos { get; set; }

    public virtual DbSet<productoEntrada> productoEntrada { get; set; }

    public virtual DbSet<proveedores> proveedores { get; set; }

    public virtual DbSet<reciboPago> reciboPagos { get; set; }

    public virtual DbSet<reciboVenta> reciboVenta { get; set; }

    public virtual DbSet<salidaProducto> salidaProductos { get; set; }

    public virtual DbSet<stock> stocks { get; set; }

    public virtual DbSet<subCategoria> subCategorias { get; set; }

    public virtual DbSet<usuario> usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<pedido>()
        .ToTable(tb => tb.UseSqlOutputClause(false));

        // Si también tienes trigger en detallesPedido
        modelBuilder.Entity<detallesPedido>()
            .ToTable(tb => tb.UseSqlOutputClause(false));

        modelBuilder.Entity<categoria>(entity =>
        {
            entity.HasKey(e => e.codigoCategorias).HasName("PK__categori__612FD8B17F0BC168");
        });

        modelBuilder.Entity<cliente>(entity =>
        {
            entity.HasKey(e => e.documentoCliente).HasName("PK__cliente__21348785AC5F7CB2");
        });

        modelBuilder.Entity<detallesPedido>(entity =>
        {
            entity.HasKey(e => new { e.codigoPedido, e.codigoProducto }).HasName("PK__detalles__0A1ABEFCC921DE8B");

            entity.HasOne(d => d.codigoPedidoNavigation).WithMany(p => p.detallesPedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_detalles_pedidos");

            entity.HasOne(d => d.codigoProductoNavigation).WithMany(p => p.detallesPedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_detalles_producto");
        });

        modelBuilder.Entity<detallesRecibo>(entity =>
        {
            entity.HasKey(e => new { e.codigoReciboVenta, e.codigoProducto }).HasName("PK__detalles__A884E8EF75E22B97");

            entity.HasOne(d => d.codigoProductoNavigation).WithMany(p => p.detallesRecibos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_detallesRecibo_producto");

            entity.HasOne(d => d.codigoReciboVentaNavigation).WithMany(p => p.detallesRecibos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_detallesRecibo_reciboVenta");
        });

        modelBuilder.Entity<entradaProducto>(entity =>
        {
            entity.HasKey(e => e.codigoEntrada).HasName("PK__entradaP__F48E68AAC78E1E82");

            entity.HasOne(d => d.codigoProveedorNavigation).WithMany(p => p.entradaProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_entradaProducto_proveedores");
        });

        modelBuilder.Entity<formaPago>(entity =>
        {
            entity.HasKey(e => e.codigoFormaPago).HasName("PK__formaPag__48B185D30D670718");
        });

        modelBuilder.Entity<movimientosStock>(entity =>
        {
            entity.HasKey(e => e.codigoMovimientoStock).HasName("PK__movimien__0C6251D60FE04731");

            entity.HasOne(d => d.codigoEntradaNavigation).WithMany(p => p.movimientosStocks).HasConstraintName("FK_movimientosStock_entradaProducto");

            entity.HasOne(d => d.codigoReciboVentaNavigation).WithMany(p => p.movimientosStocks).HasConstraintName("FK_movimientosStock_reciboVenta");

            entity.HasOne(d => d.codigoStockNavigation).WithMany(p => p.movimientosStocks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_movimientosStock_stock");
        });

        modelBuilder.Entity<niveles>(entity =>
        {
            entity.HasKey(e => e.codigoNivel).HasName("PK__niveles__D9D29D0CF09F9E6C");
        });

        modelBuilder.Entity<pedido>(entity =>
        {
            entity.HasKey(e => e.codigoPedido).HasName("PK__pedidos__01D75982835F2DCC");

            entity.HasOne(d => d.documentoClienteNavigation).WithMany(p => p.pedidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pedidos_cliente");
        });

        modelBuilder.Entity<producto>(entity =>
        {
            entity.HasKey(e => e.codigoProducto).HasName("PK__producto__BCDE77E131A25745");

            entity.HasOne(d => d.codigoSubCategoriasNavigation).WithMany(p => p.productos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_producto_subCategorias");

            entity.HasOne(d => d.documentoProveedorNavigation).WithMany(p => p.productos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_producto_proveedores");
        });

        modelBuilder.Entity<productoEntrada>(entity =>
        {
            entity.HasKey(e => new { e.codigoProducto, e.codigoEntrada }).HasName("PK__producto__0396916B49461B0F");

            entity.HasOne(d => d.codigoEntradaNavigation).WithMany(p => p.productoEntrada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_productoEntrada_entradaProducto");

            entity.HasOne(d => d.codigoProductoNavigation).WithMany(p => p.productoEntrada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_productoEntrada_producto");
        });

        modelBuilder.Entity<proveedores>(entity =>
        {
            entity.HasKey(e => e.documentoProveedor).HasName("PK__proveedo__693B041D2588F88B");
        });

        modelBuilder.Entity<reciboPago>(entity =>
        {
            entity.HasKey(e => new { e.codigoReciboVenta, e.codigoFormaPago }).HasName("PK__reciboPa__87C217CC60D04857");

            entity.HasOne(d => d.codigoFormaPagoNavigation).WithMany(p => p.reciboPagos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reciboPagos_formaPago");

            entity.HasOne(d => d.codigoReciboVentaNavigation).WithMany(p => p.reciboPagos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reciboPagos_reciboVenta");
        });

        modelBuilder.Entity<reciboVenta>(entity =>
        {
            entity.HasKey(e => e.codigoReciboVenta).HasName("PK__reciboVe__A3490F91903EC6B6");

            entity.HasOne(d => d.codigoPedidoNavigation).WithMany(p => p.reciboVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reciboVenta_pedidos");
        });

        modelBuilder.Entity<salidaProducto>(entity =>
        {
            entity.HasKey(e => new { e.codigoReciboVenta, e.codigoProducto }).HasName("PK__salidaPr__A884E8EF457C5AE9");

            entity.HasOne(d => d.codigoProductoNavigation).WithMany(p => p.salidaProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_salidaProducto_producto");

            entity.HasOne(d => d.codigoReciboVentaNavigation).WithMany(p => p.salidaProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_salidaProducto_reciboVenta");
        });

        modelBuilder.Entity<stock>(entity =>
        {
            entity.HasKey(e => e.codigoStock).HasName("PK__stock__9341CE2E87F21FBF");

            entity.HasOne(d => d.codigoProductoNavigation).WithMany(p => p.stocks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_stock_producto");
        });

        modelBuilder.Entity<subCategoria>(entity =>
        {
            entity.HasKey(e => e.codigoSubCategorias).HasName("PK__subCateg__49277B855EFC3AFD");

            entity.HasOne(d => d.codigoCategoriasNavigation).WithMany(p => p.subCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_subCategorias_categorias");
        });

        modelBuilder.Entity<usuario>(entity =>
        {
            entity.HasKey(e => e.documentoUsuario).HasName("PK__usuario__F1843332CDBA3BE9");

            entity.HasOne(d => d.codigoNivelNavigation).WithMany(p => p.usuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usuario_niveles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
