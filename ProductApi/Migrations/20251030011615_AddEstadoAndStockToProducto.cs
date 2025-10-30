using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class AddEstadoAndStockToProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Productos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Productos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Productos");
        }
    }
}
