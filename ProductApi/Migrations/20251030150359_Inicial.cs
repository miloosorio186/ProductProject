using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagenesProductos");

            migrationBuilder.DropColumn(
                name: "ImagenUrl",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "Productos",
                newName: "Stock");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Productos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Productos",
                newName: "Cantidad");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Productos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "ImagenUrl",
                table: "Productos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImagenesProductos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagenesProductos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagenesProductos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagenesProductos_ProductoId",
                table: "ImagenesProductos",
                column: "ProductoId");
        }
    }
}
