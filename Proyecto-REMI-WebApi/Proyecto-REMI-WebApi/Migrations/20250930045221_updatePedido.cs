using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_REMI_WebApi.Migrations
{
    /// <inheritdoc />
    public partial class updatePedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "totalVenta",
                table: "reciboVenta",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "valorPago",
                table: "reciboPagos",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "precioProducto",
                table: "producto",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "valorPedido",
                table: "pedidos",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "totalVenta",
                table: "reciboVenta",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<double>(
                name: "valorPago",
                table: "reciboPagos",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<double>(
                name: "precioProducto",
                table: "producto",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<double>(
                name: "valorPedido",
                table: "pedidos",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");
        }
    }
}
