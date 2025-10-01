using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_REMI_WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    codigoCategorias = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreCategorias = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__categori__612FD8B17F0BC168", x => x.codigoCategorias);
                });

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    documentoCliente = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    tipoDocumentoCliente = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    nombreCliente = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    apellidoCliente = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    correoCliente = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    telefonoCliente = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cliente__21348785AC5F7CB2", x => x.documentoCliente);
                });

            migrationBuilder.CreateTable(
                name: "formaPago",
                columns: table => new
                {
                    codigoFormaPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreFormaPago = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__formaPag__48B185D30D670718", x => x.codigoFormaPago);
                });

            migrationBuilder.CreateTable(
                name: "niveles",
                columns: table => new
                {
                    codigoNivel = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreNivel = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__niveles__D9D29D0CF09F9E6C", x => x.codigoNivel);
                });

            migrationBuilder.CreateTable(
                name: "proveedores",
                columns: table => new
                {
                    documentoProveedor = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    tipoDocumentoProveedor = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    nombreProveedor = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    correoProveedor = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    telefonoProveedor = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__proveedo__693B041D2588F88B", x => x.documentoProveedor);
                });

            migrationBuilder.CreateTable(
                name: "subCategorias",
                columns: table => new
                {
                    codigoSubCategorias = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreSubCategorias = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    codigoCategorias = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__subCateg__49277B855EFC3AFD", x => x.codigoSubCategorias);
                    table.ForeignKey(
                        name: "FK_subCategorias_categorias",
                        column: x => x.codigoCategorias,
                        principalTable: "categorias",
                        principalColumn: "codigoCategorias");
                });

            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    codigoPedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaPedido = table.Column<DateOnly>(type: "date", nullable: false),
                    horaPedido = table.Column<TimeOnly>(type: "time", nullable: false),
                    valorPedido = table.Column<double>(type: "float", nullable: false),
                    documentoCliente = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    estadoPedido = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__pedidos__01D75982835F2DCC", x => x.codigoPedido);
                    table.ForeignKey(
                        name: "FK_pedidos_cliente",
                        column: x => x.documentoCliente,
                        principalTable: "cliente",
                        principalColumn: "documentoCliente");
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    documentoUsuario = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    tipoDocumentoUsuario = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    nombreUsuario = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    apellidoUsuario = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    correoUsuario = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    password = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    codigoNivel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuario__F1843332CDBA3BE9", x => x.documentoUsuario);
                    table.ForeignKey(
                        name: "FK_usuario_niveles",
                        column: x => x.codigoNivel,
                        principalTable: "niveles",
                        principalColumn: "codigoNivel");
                });

            migrationBuilder.CreateTable(
                name: "entradaProducto",
                columns: table => new
                {
                    codigoEntrada = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaEntrada = table.Column<DateOnly>(type: "date", nullable: false),
                    numeroFacturaEntrada = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    codigoProveedor = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__entradaP__F48E68AAC78E1E82", x => x.codigoEntrada);
                    table.ForeignKey(
                        name: "FK_entradaProducto_proveedores",
                        column: x => x.codigoProveedor,
                        principalTable: "proveedores",
                        principalColumn: "documentoProveedor");
                });

            migrationBuilder.CreateTable(
                name: "producto",
                columns: table => new
                {
                    codigoProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreProducto = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    entradaProducto = table.Column<int>(type: "int", nullable: true),
                    salidaProducto = table.Column<int>(type: "int", nullable: true),
                    marcaProducto = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    precioProducto = table.Column<double>(type: "float", nullable: true),
                    codigoSubCategorias = table.Column<int>(type: "int", nullable: false),
                    documentoProveedor = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__producto__BCDE77E131A25745", x => x.codigoProducto);
                    table.ForeignKey(
                        name: "FK_producto_proveedores",
                        column: x => x.documentoProveedor,
                        principalTable: "proveedores",
                        principalColumn: "documentoProveedor");
                    table.ForeignKey(
                        name: "FK_producto_subCategorias",
                        column: x => x.codigoSubCategorias,
                        principalTable: "subCategorias",
                        principalColumn: "codigoSubCategorias");
                });

            migrationBuilder.CreateTable(
                name: "reciboVenta",
                columns: table => new
                {
                    codigoReciboVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaReciboVenta = table.Column<DateOnly>(type: "date", nullable: false),
                    horaReciboVenta = table.Column<TimeOnly>(type: "time", nullable: false),
                    valorVenta = table.Column<double>(type: "float", nullable: false),
                    totalVenta = table.Column<double>(type: "float", nullable: false),
                    codigoPedido = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__reciboVe__A3490F91903EC6B6", x => x.codigoReciboVenta);
                    table.ForeignKey(
                        name: "FK_reciboVenta_pedidos",
                        column: x => x.codigoPedido,
                        principalTable: "pedidos",
                        principalColumn: "codigoPedido");
                });

            migrationBuilder.CreateTable(
                name: "detallesPedido",
                columns: table => new
                {
                    codigoPedido = table.Column<int>(type: "int", nullable: false),
                    codigoProducto = table.Column<int>(type: "int", nullable: false),
                    cantidadProducto = table.Column<double>(type: "float", nullable: false),
                    valorProducto = table.Column<double>(type: "float", nullable: false),
                    totalPagoProducto = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__detalles__0A1ABEFCC921DE8B", x => new { x.codigoPedido, x.codigoProducto });
                    table.ForeignKey(
                        name: "FK_detalles_pedidos",
                        column: x => x.codigoPedido,
                        principalTable: "pedidos",
                        principalColumn: "codigoPedido");
                    table.ForeignKey(
                        name: "FK_detalles_producto",
                        column: x => x.codigoProducto,
                        principalTable: "producto",
                        principalColumn: "codigoProducto");
                });

            migrationBuilder.CreateTable(
                name: "productoEntrada",
                columns: table => new
                {
                    codigoProducto = table.Column<int>(type: "int", nullable: false),
                    codigoEntrada = table.Column<int>(type: "int", nullable: false),
                    cantidadProductoEntrada = table.Column<int>(type: "int", nullable: false),
                    valorUnitarioProductoEntrada = table.Column<double>(type: "float", nullable: false),
                    totalProductoEntrada = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__producto__0396916B49461B0F", x => new { x.codigoProducto, x.codigoEntrada });
                    table.ForeignKey(
                        name: "FK_productoEntrada_entradaProducto",
                        column: x => x.codigoEntrada,
                        principalTable: "entradaProducto",
                        principalColumn: "codigoEntrada");
                    table.ForeignKey(
                        name: "FK_productoEntrada_producto",
                        column: x => x.codigoProducto,
                        principalTable: "producto",
                        principalColumn: "codigoProducto");
                });

            migrationBuilder.CreateTable(
                name: "stock",
                columns: table => new
                {
                    codigoStock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stockMin = table.Column<int>(type: "int", nullable: false),
                    stockMax = table.Column<int>(type: "int", nullable: false),
                    cantidadActual = table.Column<int>(type: "int", nullable: false),
                    estadoStock = table.Column<byte>(type: "tinyint", nullable: false),
                    codigoProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__stock__9341CE2E87F21FBF", x => x.codigoStock);
                    table.ForeignKey(
                        name: "FK_stock_producto",
                        column: x => x.codigoProducto,
                        principalTable: "producto",
                        principalColumn: "codigoProducto");
                });

            migrationBuilder.CreateTable(
                name: "detallesRecibo",
                columns: table => new
                {
                    codigoReciboVenta = table.Column<int>(type: "int", nullable: false),
                    codigoProducto = table.Column<int>(type: "int", nullable: false),
                    cantidadProductoRecibo = table.Column<double>(type: "float", nullable: false),
                    valorUnitarioRecibo = table.Column<double>(type: "float", nullable: false),
                    totalProductoRecibo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__detalles__A884E8EF75E22B97", x => new { x.codigoReciboVenta, x.codigoProducto });
                    table.ForeignKey(
                        name: "FK_detallesRecibo_producto",
                        column: x => x.codigoProducto,
                        principalTable: "producto",
                        principalColumn: "codigoProducto");
                    table.ForeignKey(
                        name: "FK_detallesRecibo_reciboVenta",
                        column: x => x.codigoReciboVenta,
                        principalTable: "reciboVenta",
                        principalColumn: "codigoReciboVenta");
                });

            migrationBuilder.CreateTable(
                name: "reciboPagos",
                columns: table => new
                {
                    codigoReciboVenta = table.Column<int>(type: "int", nullable: false),
                    codigoFormaPago = table.Column<int>(type: "int", nullable: false),
                    valorPago = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__reciboPa__87C217CC60D04857", x => new { x.codigoReciboVenta, x.codigoFormaPago });
                    table.ForeignKey(
                        name: "FK_reciboPagos_formaPago",
                        column: x => x.codigoFormaPago,
                        principalTable: "formaPago",
                        principalColumn: "codigoFormaPago");
                    table.ForeignKey(
                        name: "FK_reciboPagos_reciboVenta",
                        column: x => x.codigoReciboVenta,
                        principalTable: "reciboVenta",
                        principalColumn: "codigoReciboVenta");
                });

            migrationBuilder.CreateTable(
                name: "salidaProducto",
                columns: table => new
                {
                    codigoReciboVenta = table.Column<int>(type: "int", nullable: false),
                    codigoProducto = table.Column<int>(type: "int", nullable: false),
                    cantidadSalidaProducto = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__salidaPr__A884E8EF457C5AE9", x => new { x.codigoReciboVenta, x.codigoProducto });
                    table.ForeignKey(
                        name: "FK_salidaProducto_producto",
                        column: x => x.codigoProducto,
                        principalTable: "producto",
                        principalColumn: "codigoProducto");
                    table.ForeignKey(
                        name: "FK_salidaProducto_reciboVenta",
                        column: x => x.codigoReciboVenta,
                        principalTable: "reciboVenta",
                        principalColumn: "codigoReciboVenta");
                });

            migrationBuilder.CreateTable(
                name: "movimientosStock",
                columns: table => new
                {
                    codigoMovimientoStock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoMovimientoStock = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    fechaMovimientoStock = table.Column<DateTime>(type: "datetime", nullable: false),
                    referenciaMovimientoStock = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    codigoStock = table.Column<int>(type: "int", nullable: false),
                    codigoReciboVenta = table.Column<int>(type: "int", nullable: true),
                    codigoEntrada = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__movimien__0C6251D60FE04731", x => x.codigoMovimientoStock);
                    table.ForeignKey(
                        name: "FK_movimientosStock_entradaProducto",
                        column: x => x.codigoEntrada,
                        principalTable: "entradaProducto",
                        principalColumn: "codigoEntrada");
                    table.ForeignKey(
                        name: "FK_movimientosStock_reciboVenta",
                        column: x => x.codigoReciboVenta,
                        principalTable: "reciboVenta",
                        principalColumn: "codigoReciboVenta");
                    table.ForeignKey(
                        name: "FK_movimientosStock_stock",
                        column: x => x.codigoStock,
                        principalTable: "stock",
                        principalColumn: "codigoStock");
                });

            migrationBuilder.CreateIndex(
                name: "IX_detallesPedido_codigoProducto",
                table: "detallesPedido",
                column: "codigoProducto");

            migrationBuilder.CreateIndex(
                name: "IX_detallesRecibo_codigoProducto",
                table: "detallesRecibo",
                column: "codigoProducto");

            migrationBuilder.CreateIndex(
                name: "IX_entradaProducto_codigoProveedor",
                table: "entradaProducto",
                column: "codigoProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_movimientosStock_codigoEntrada",
                table: "movimientosStock",
                column: "codigoEntrada");

            migrationBuilder.CreateIndex(
                name: "IX_movimientosStock_codigoReciboVenta",
                table: "movimientosStock",
                column: "codigoReciboVenta");

            migrationBuilder.CreateIndex(
                name: "IX_movimientosStock_codigoStock",
                table: "movimientosStock",
                column: "codigoStock");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_documentoCliente",
                table: "pedidos",
                column: "documentoCliente");

            migrationBuilder.CreateIndex(
                name: "IX_producto_codigoSubCategorias",
                table: "producto",
                column: "codigoSubCategorias");

            migrationBuilder.CreateIndex(
                name: "IX_producto_documentoProveedor",
                table: "producto",
                column: "documentoProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_productoEntrada_codigoEntrada",
                table: "productoEntrada",
                column: "codigoEntrada");

            migrationBuilder.CreateIndex(
                name: "IX_reciboPagos_codigoFormaPago",
                table: "reciboPagos",
                column: "codigoFormaPago");

            migrationBuilder.CreateIndex(
                name: "IX_reciboVenta_codigoPedido",
                table: "reciboVenta",
                column: "codigoPedido");

            migrationBuilder.CreateIndex(
                name: "IX_salidaProducto_codigoProducto",
                table: "salidaProducto",
                column: "codigoProducto");

            migrationBuilder.CreateIndex(
                name: "IX_stock_codigoProducto",
                table: "stock",
                column: "codigoProducto");

            migrationBuilder.CreateIndex(
                name: "IX_subCategorias_codigoCategorias",
                table: "subCategorias",
                column: "codigoCategorias");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_codigoNivel",
                table: "usuario",
                column: "codigoNivel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detallesPedido");

            migrationBuilder.DropTable(
                name: "detallesRecibo");

            migrationBuilder.DropTable(
                name: "movimientosStock");

            migrationBuilder.DropTable(
                name: "productoEntrada");

            migrationBuilder.DropTable(
                name: "reciboPagos");

            migrationBuilder.DropTable(
                name: "salidaProducto");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "stock");

            migrationBuilder.DropTable(
                name: "entradaProducto");

            migrationBuilder.DropTable(
                name: "formaPago");

            migrationBuilder.DropTable(
                name: "reciboVenta");

            migrationBuilder.DropTable(
                name: "niveles");

            migrationBuilder.DropTable(
                name: "producto");

            migrationBuilder.DropTable(
                name: "pedidos");

            migrationBuilder.DropTable(
                name: "proveedores");

            migrationBuilder.DropTable(
                name: "subCategorias");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "categorias");
        }
    }
}
