using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Pessoa_Usuario_EFCore.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdAlternativo = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    TenantId = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    ApelidoFantasia = table.Column<string>(type: "VARCHAR(60)", nullable: true),
                    FisJur = table.Column<int>(type: "INT", nullable: false, defaultValue: 0),
                    DocCpfCnpj = table.Column<string>(type: "VARCHAR(11)", nullable: false),
                    DocIdentidade = table.Column<string>(type: "VARCHAR(11)", nullable: true),
                    DocIdentidadeEmissor = table.Column<string>(type: "VARCHAR(10)", nullable: true),
                    DocInscricaoEstadual = table.Column<string>(type: "VARCHAR(15)", nullable: true, defaultValue: "ISENTO"),
                    DocInscricaoMunicipal = table.Column<string>(type: "VARCHAR(15)", nullable: true),
                    DocInscricaoProdutorRural = table.Column<string>(type: "text", nullable: true),
                    DocInscSuframa = table.Column<string>(type: "VARCHAR(15)", nullable: true),
                    NascimentoFundacao = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: true),
                    EstadoCivil = table.Column<int>(type: "INT", nullable: true),
                    NomeConjuge = table.Column<string>(type: "VARCHAR(60)", nullable: true),
                    NomePai = table.Column<string>(type: "VARCHAR(60)", nullable: true),
                    NomeMae = table.Column<string>(type: "VARCHAR(60)", nullable: true),
                    Genero = table.Column<int>(type: "INT", nullable: true, defaultValue: 11),
                    Site = table.Column<string>(type: "VARCHAR(75)", nullable: true),
                    Observacao = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Ativo = table.Column<bool>(type: "BOOL", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "UUID", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "UUID", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "UUID", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PessoaId = table.Column<Guid>(type: "UUID", nullable: false),
                    TenantId = table.Column<Guid>(type: "UUID", nullable: false),
                    Apelido = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    CodigoAlternativo = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "UUID", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "UUID", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "UUID", nullable: true),
                    Ativo = table.Column<bool>(type: "BOOL", nullable: false, defaultValue: true),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_pes_tenant",
                table: "Pessoas",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "idx_pessoa_id_alternativo",
                table: "Pessoas",
                column: "IdAlternativo");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_DocInscricaoProdutorRural",
                table: "Pessoas",
                column: "DocInscricaoProdutorRural",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "pk_pessoa",
                table: "Pessoas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "unq_pessoa_1",
                table: "Pessoas",
                column: "DocCpfCnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "unq_pessoa_2",
                table: "Pessoas",
                columns: new[] { "DocInscricaoProdutorRural", "DocCpfCnpj" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_usuario_tenant",
                table: "Usuarios",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "pk_usuario",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "usu_ix_pessoa",
                table: "Usuarios",
                column: "PessoaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
