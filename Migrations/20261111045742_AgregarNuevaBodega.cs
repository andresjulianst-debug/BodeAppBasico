using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BodeAppBasico.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNuevaBodega : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bodegas",
                columns: new[] { "Id", "EsCentral", "Nombre" },
                values: new object[] { 5, false, "5 - Guarne" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bodegas",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
