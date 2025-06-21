using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgenTurismo.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletedAtToPacoteTuristico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Clientes_ClienteId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_PacotesTuristicos_PacoteTuristicoId",
                table: "Reservas");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "PacotesTuristicos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 1000m,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataInicio",
                table: "PacotesTuristicos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 26, 23, 10, 32, 266, DateTimeKind.Local).AddTicks(8837),
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "CapacidadeMaxima",
                table: "PacotesTuristicos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 10,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "PacotesTuristicos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PacotesTuristicos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Clientes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Clientes_ClienteId",
                table: "Reservas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_PacotesTuristicos_PacoteTuristicoId",
                table: "Reservas",
                column: "PacoteTuristicoId",
                principalTable: "PacotesTuristicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Clientes_ClienteId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_PacotesTuristicos_PacoteTuristicoId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "PacotesTuristicos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PacotesTuristicos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Clientes");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "PacotesTuristicos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 1000m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataInicio",
                table: "PacotesTuristicos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2025, 6, 26, 23, 10, 32, 266, DateTimeKind.Local).AddTicks(8837));

            migrationBuilder.AlterColumn<int>(
                name: "CapacidadeMaxima",
                table: "PacotesTuristicos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 10);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Clientes_ClienteId",
                table: "Reservas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_PacotesTuristicos_PacoteTuristicoId",
                table: "Reservas",
                column: "PacoteTuristicoId",
                principalTable: "PacotesTuristicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
