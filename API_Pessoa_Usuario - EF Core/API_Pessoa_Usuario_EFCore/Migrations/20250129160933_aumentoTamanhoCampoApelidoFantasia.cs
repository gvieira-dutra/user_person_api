using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Pessoa_Usuario_EFCore.Migrations
{
    /// <inheritdoc />
    public partial class aumentoTamanhoCampoApelidoFantasia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApelidoFantasia",
                table: "Pessoas",
                type: "VARCHAR(75)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApelidoFantasia",
                table: "Pessoas",
                type: "VARCHAR(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(75)",
                oldNullable: true);
        }
    }
}
