using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mini_Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataToTareas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tareas",
                columns: new[] { "Id", "Nombredelatarea", "FechadeInicio", "tiempoestimado", "EstadoProgreso", "proyectoId", "empleadoId" },
                values: new object[,]
                {
                    { 1, "Tarea 1", new DateTime(2024, 3, 1), 5.5, "En progreso", 1, 1 },
                    { 2, "Tarea 2", new DateTime(2024, 2, 2), 7.5, "Completado", 2, 2 },
                    { 3, "Tarea 3", new DateTime(2024, 1, 3), 3.0, "En espera", 3, 1 },
                    { 4, "Tarea 4", new DateTime(2024, 3, 4), 4.5, "En progreso", 1, 4 },
                    { 5, "Tarea 5", new DateTime(2024, 6, 5), 6.0, "Completado", 4, 5 },
                    { 6, "Tarea 6", new DateTime(2024, 5, 6), 2.5, "En progreso", 2, 2 },
                    { 7, "Tarea 7", new DateTime(2024, 2, 7), 8.0, "En espera", 3, 1 },
                    { 8, "Tarea 8", new DateTime(2024, 1, 8), 4.0, "En progreso", 1, 4 },
                    { 9, "Tarea 9", new DateTime(2024, 2, 9), 5.5, "Completado", 2, 1 },
                    { 10, "Tarea 10", new DateTime(2024, 10, 10), 7.0, "En progreso", 3, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tareas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tareas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tareas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tareas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tareas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tareas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tareas",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tareas",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tareas",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tareas",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
