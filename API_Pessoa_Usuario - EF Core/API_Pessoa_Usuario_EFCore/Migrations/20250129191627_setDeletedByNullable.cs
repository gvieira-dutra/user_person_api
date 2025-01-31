using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Pessoa_Usuario_EFCore.Migrations
{
    /// <inheritdoc />
    public partial class setDeletedByNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "DeletedBy",
                table: "Pessoas",
                type: "UUID",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "UUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "DeletedBy",
                table: "Pessoas",
                type: "UUID",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "UUID",
                oldNullable: true);
        }
    }
}
