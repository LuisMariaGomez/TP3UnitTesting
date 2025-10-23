using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CDatos.Migrations
{
    /// <inheritdoc />
    public partial class add1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zombie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    NivelPeligro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Velocidad = table.Column<double>(type: "float", nullable: false),
                    FechaInfeccion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zombie", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Zombie",
                columns: new[] { "Id", "Edad", "Estado", "FechaInfeccion", "NivelPeligro", "Nombre", "Tipo", "Velocidad" },
                values: new object[,]
                {
                    { 1, 24, "Vivo", new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bajo", "Rickon", "Caminante", 3.2000000000000002 },
                    { 2, 30, "Vivo", new DateTime(2024, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medio", "Luna", "Corredor", 7.0999999999999996 },
                    { 3, 45, "Eliminado", new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alto", "Brutus", "Mutante", 2.5 },
                    { 4, 27, "Vivo", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medio", "Marla", "Caminante", 4.0 },
                    { 5, 51, "Eliminado", new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alto", "Gnasher", "Radioactivo", 1.8 },
                    { 6, 32, "Vivo", new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medio", "Corvus", "Corredor", 8.0 },
                    { 7, 19, "Vivo", new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bajo", "Elsa", "Caminante", 2.8999999999999999 },
                    { 8, 60, "Eliminado", new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alto", "Tor", "Mutante", 2.0 },
                    { 9, 34, "Vivo", new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medio", "Sable", "Corredor", 6.2999999999999998 },
                    { 10, 29, "Vivo", new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bajo", "Vera", "Caminante", 3.5 },
                    { 11, 38, "Eliminado", new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alto", "Hawk", "Mutante", 2.7999999999999998 },
                    { 12, 25, "Vivo", new DateTime(2024, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medio", "Dina", "Corredor", 7.5 },
                    { 13, 41, "Eliminado", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alto", "Kragg", "Radioactivo", 1.5 },
                    { 14, 22, "Vivo", new DateTime(2024, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bajo", "Milo", "Caminante", 3.1000000000000001 },
                    { 15, 35, "Vivo", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medio", "Rosa", "Corredor", 6.9000000000000004 },
                    { 16, 50, "Eliminado", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alto", "Xar", "Mutante", 2.2999999999999998 },
                    { 17, 31, "Vivo", new DateTime(2024, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medio", "Ivy", "Caminante", 4.2000000000000002 },
                    { 18, 40, "Eliminado", new DateTime(2024, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alto", "Nox", "Radioactivo", 1.8999999999999999 },
                    { 19, 28, "Vivo", new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bajo", "Kira", "Corredor", 5.7999999999999998 },
                    { 20, 36, "Eliminado", new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alto", "Omen", "Mutante", 2.6000000000000001 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zombie");
        }
    }
}
