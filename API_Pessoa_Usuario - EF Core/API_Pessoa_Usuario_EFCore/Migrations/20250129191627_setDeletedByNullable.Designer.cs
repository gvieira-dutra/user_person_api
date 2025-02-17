﻿// <auto-generated />
using System;
using API_Pessoa_Usuario_EFCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API_Pessoa_Usuario_EFCore.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20250129191627_setDeletedByNullable")]
    partial class setDeletedByNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API_Pessoa_Usuario_EFCore.Domain.Pessoa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApelidoFantasia")
                        .HasColumnType("VARCHAR(75)");

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BOOL")
                        .HasDefaultValue(true);

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("UUID");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TIMESTAMPTZ");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("UUID");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("TIMESTAMPTZ");

                    b.Property<string>("DocCpfCnpj")
                        .IsRequired()
                        .HasColumnType("VARCHAR(11)");

                    b.Property<string>("DocIdentidade")
                        .HasColumnType("VARCHAR(11)");

                    b.Property<string>("DocIdentidadeEmissor")
                        .HasColumnType("VARCHAR(10)");

                    b.Property<string>("DocInscSuframa")
                        .HasColumnType("VARCHAR(15)");

                    b.Property<string>("DocInscricaoEstadual")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(15)")
                        .HasDefaultValue("ISENTO");

                    b.Property<string>("DocInscricaoMunicipal")
                        .HasColumnType("VARCHAR(15)");

                    b.Property<string>("DocInscricaoProdutorRural")
                        .HasColumnType("text");

                    b.Property<int?>("EstadoCivil")
                        .HasColumnType("INT");

                    b.Property<int>("FisJur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasDefaultValue(0);

                    b.Property<int?>("Genero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasDefaultValue(11);

                    b.Property<string>("IdAlternativo")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)");

                    b.Property<Guid>("LastModifiedBy")
                        .HasColumnType("UUID");

                    b.Property<DateTime>("LastModifiedOn")
                        .HasColumnType("TIMESTAMPTZ");

                    b.Property<DateTime?>("NascimentoFundacao")
                        .HasColumnType("TIMESTAMPTZ");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(60)");

                    b.Property<string>("NomeConjuge")
                        .HasColumnType("VARCHAR(60)");

                    b.Property<string>("NomeMae")
                        .HasColumnType("VARCHAR(60)");

                    b.Property<string>("NomePai")
                        .HasColumnType("VARCHAR(60)");

                    b.Property<string>("Observacao")
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("Site")
                        .HasColumnType("VARCHAR(75)");

                    b.Property<string>("TenantId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)");

                    b.HasKey("Id");

                    b.HasIndex("DocCpfCnpj")
                        .IsUnique()
                        .HasDatabaseName("unq_pessoa_1");

                    b.HasIndex("DocInscricaoProdutorRural")
                        .IsUnique();

                    b.HasIndex("Id")
                        .HasDatabaseName("pk_pessoa");

                    b.HasIndex("IdAlternativo")
                        .HasDatabaseName("idx_pessoa_id_alternativo");

                    b.HasIndex("TenantId")
                        .HasDatabaseName("idx_pes_tenant");

                    b.HasIndex("DocInscricaoProdutorRural", "DocCpfCnpj")
                        .IsUnique()
                        .HasDatabaseName("unq_pessoa_2");

                    b.ToTable("Pessoas", (string)null);
                });

            modelBuilder.Entity("API_Pessoa_Usuario_EFCore.Domain.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Apelido")
                        .HasColumnType("VARCHAR(30)");

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BOOL")
                        .HasDefaultValue(true);

                    b.Property<string>("CodigoAlternativo")
                        .HasColumnType("VARCHAR(50)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("UUID");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TIMESTAMPTZ");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("UUID");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("TIMESTAMPTZ");

                    b.Property<Guid>("LastModifiedBy")
                        .HasColumnType("UUID");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PessoaId")
                        .HasColumnType("UUID");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("UUID");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .HasDatabaseName("pk_usuario");

                    b.HasIndex("PessoaId")
                        .IsUnique()
                        .HasDatabaseName("usu_ix_pessoa");

                    b.HasIndex("TenantId")
                        .HasDatabaseName("idx_usuario_tenant");

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("API_Pessoa_Usuario_EFCore.Domain.Usuario", b =>
                {
                    b.HasOne("API_Pessoa_Usuario_EFCore.Domain.Pessoa", "PessoaPertencente")
                        .WithOne("Usuario")
                        .HasForeignKey("API_Pessoa_Usuario_EFCore.Domain.Usuario", "PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PessoaPertencente");
                });

            modelBuilder.Entity("API_Pessoa_Usuario_EFCore.Domain.Pessoa", b =>
                {
                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
