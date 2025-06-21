using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgenTurismo.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoRelacionamentosExclusaoLogica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataInicio",
                table: "PacotesTuristicos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 26, 23, 18, 20, 149, DateTimeKind.Local).AddTicks(2275),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2025, 6, 26, 23, 10, 32, 266, DateTimeKind.Local).AddTicks(8837));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataInicio",
                table: "PacotesTuristicos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 26, 23, 10, 32, 266, DateTimeKind.Local).AddTicks(8837),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2025, 6, 26, 23, 18, 20, 149, DateTimeKind.Local).AddTicks(2275));
        }
    }
}
