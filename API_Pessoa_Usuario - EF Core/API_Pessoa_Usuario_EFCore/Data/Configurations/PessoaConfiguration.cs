using API_Pessoa_Usuario_EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Pessoa_Usuario_EFCore.Data.Configurations;

public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.ToTable("Pessoas");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.IdAlternativo).HasColumnType("VARCHAR(36)");
        builder.Property(p => p.TenantId).HasColumnType("VARCHAR(36)").IsRequired();
        builder.Property(p => p.Nome).HasColumnType("VARCHAR(60)").IsRequired();
        builder.Property(p => p.ApelidoFantasia).HasColumnType("VARCHAR(75)");
        builder.Property(p => p.FisJur).HasColumnType("INT").IsRequired().HasDefaultValue(0);

        builder.Property(p => p.DocCpfCnpj).HasColumnType("VARCHAR(11)");
        builder.Property(p => p.DocIdentidade).HasColumnType("VARCHAR(11)");
        builder.Property(p => p.DocIdentidadeEmissor).HasColumnType("VARCHAR(10)");
        builder.Property(p => p.DocInscricaoEstadual).HasColumnType("VARCHAR(15)").HasDefaultValue("ISENTO");
        builder.Property(p => p.DocInscricaoMunicipal).HasColumnType("VARCHAR(15)");
        builder.Property(p => p.DocInscSuframa).HasColumnType("VARCHAR(15)");

        builder.Property(p => p.NascimentoFundacao).HasColumnType("TIMESTAMPTZ");
        builder.Property(p => p.EstadoCivil).HasColumnType("INT");
        builder.Property(p => p.NomeConjuge).HasColumnType("VARCHAR(60)");
        builder.Property(p => p.NomePai).HasColumnType("VARCHAR(60)");
        builder.Property(p => p.NomeMae).HasColumnType("VARCHAR(60)");
        builder.Property(p => p.Genero).HasColumnType("INT").HasDefaultValue(11);
        builder.Property(p => p.Site).HasColumnType("VARCHAR(75)");
        builder.Property(p => p.Observacao).HasColumnType("VARCHAR(255)");
        
        builder.Property(p => p.Ativo).HasColumnType("BOOL").IsRequired().HasDefaultValue(true);
        builder.Property(p => p.CreatedBy).HasColumnType("UUID").IsRequired();
        builder.Property(p => p.CreatedOn).HasColumnType("TIMESTAMPTZ").IsRequired();
        builder.Property(p => p.LastModifiedBy).HasColumnType("UUID").IsRequired();
        builder.Property(p => p.LastModifiedOn).HasColumnType("TIMESTAMPTZ").IsRequired();
        builder.Property(p => p.DeletedOn).HasColumnType("TIMESTAMPTZ");
        builder.Property(p => p.DeletedBy).HasColumnType("UUID");

        builder.HasIndex(p => p.Id).HasDatabaseName("pk_pessoa");
        builder.HasIndex(p => p.TenantId).HasDatabaseName("idx_pes_tenant");
        builder.HasIndex(p => p.IdAlternativo).HasDatabaseName("idx_pessoa_id_alternativo");

        builder.HasIndex(p => p.DocCpfCnpj).IsUnique().HasDatabaseName("unq_pessoa_1");
        builder.HasIndex(p => p.DocInscricaoProdutorRural).IsUnique();

        builder.HasIndex(p => new { p.DocInscricaoProdutorRural, p.DocCpfCnpj }).IsUnique().HasDatabaseName("unq_pessoa_2");


    }
}
