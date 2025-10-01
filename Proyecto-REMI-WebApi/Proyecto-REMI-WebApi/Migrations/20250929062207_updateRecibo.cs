using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_REMI_WebApi.Migrations
{
    /// <inheritdoc />
    public partial class updateRecibo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "documentoCliente",
                table: "reciboVenta",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "documentoCliente",
                table: "reciboVenta");
        }
    }
}
