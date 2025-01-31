using API_Pessoa_Usuario_EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Pessoa_Usuario_EFCore.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.PessoaId ).HasColumnType("UUID");
        builder.Property(p => p.TenantId ).HasColumnType("UUID").IsRequired();
        builder.Property(p => p.Apelido ).HasColumnType("VARCHAR(30)");
        builder.Property(p => p.CodigoAlternativo ).HasColumnType("VARCHAR(50)");
        builder.Property(p => p.CreatedBy ).HasColumnType("UUID").IsRequired();
        builder.Property(p => p.CreatedOn ).HasColumnType("TIMESTAMPTZ");
        builder.Property(p => p.LastModifiedBy ).HasColumnType("UUID");
        builder.Property(p => p.DeletedOn ).HasColumnType("TIMESTAMPTZ");
        builder.Property(p => p.DeletedBy ).HasColumnType("UUID");
        builder.Property(p => p.Ativo ).HasColumnType("BOOL").HasDefaultValue(true).IsRequired();

        builder.HasIndex(p => p.Id).HasDatabaseName("pk_usuario");
        builder.HasIndex(p => p.TenantId).HasDatabaseName("idx_usuario_tenant");
        builder.HasIndex(p => p.PessoaId).IsUnique().HasDatabaseName("usu_ix_pessoa");

        builder.HasOne(p => p.PessoaPertencente)
               .WithOne(p => p.Usuario)
               .HasForeignKey<Usuario>(p => p.PessoaId)
               .OnDelete(DeleteBehavior.Cascade);

    }
}
