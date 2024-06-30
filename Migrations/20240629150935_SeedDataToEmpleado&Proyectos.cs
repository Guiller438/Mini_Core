using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mini_Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataToEmpleadoProyectos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Empleados",
                columns: new[] { "id", "LastName", "Name" },
                values: new object[,]
                {
                    { 1, "Pérez", "Juan" },
                    { 2, "González", "María" },
                    { 3, "Martínez", "Carlos" },
                    { 4, "Alvarez", "Guillermo" },
                    { 5, "Torres", "Jair" }
                });

            migrationBuilder.InsertData(
                table: "Proyectos",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Proyecto A" },
                    { 2, "Proyecto B" },
                    { 3, "Proyecto C" },
                    { 4, "Proyecto E" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Proyectos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Proyectos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Proyectos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Proyectos",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
