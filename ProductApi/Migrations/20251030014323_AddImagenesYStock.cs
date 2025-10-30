using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class AddImagenesYStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "ImagenesProductos");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Productos",
                newName: "Cantidad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "Productos",
                newName: "Stock");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "ImagenesProductos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
