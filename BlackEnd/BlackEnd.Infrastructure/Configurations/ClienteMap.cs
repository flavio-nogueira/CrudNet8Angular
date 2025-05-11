using BlackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackEnd.Infrastructure.Configurations;

public class ClienteMap : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnType("char(36)")
            .IsRequired();

        builder.Property(c => c.NomeRazaoSocial)
            .HasColumnType("varchar(200)")
            .IsRequired();

        builder.Property(c => c.CpfCnpj)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(c => c.Tipo)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(c => c.DataNascimento)
            .HasColumnType("datetime(6)")
            .IsRequired(false);

        builder.Property(c => c.InscricaoEstadual)
            .HasColumnType("varchar(50)")
            .IsRequired(false);

        builder.Property(c => c.IsentoIE)
            .HasColumnType("tinyint")
            .IsRequired(false)
            .HasDefaultValue(null);

        builder.Property(c => c.Telefone)
            .HasColumnType("varchar(20)")
            .IsRequired(false);

        builder.Property(c => c.Email)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(c => c.Cep)
            .HasColumnType("varchar(10)")
            .IsRequired(false);

        builder.Property(c => c.Endereco)
            .HasColumnType("varchar(200)")
            .IsRequired(false);

        builder.Property(c => c.Numero)
            .HasColumnType("varchar(10)")
            .IsRequired(false);

        builder.Property(c => c.Bairro)
            .HasColumnType("varchar(100)")
            .IsRequired(false);

        builder.Property(c => c.Cidade)
            .HasColumnType("varchar(100)")
            .IsRequired(false);

        builder.Property(c => c.Estado)
            .HasColumnType("varchar(2)")
            .IsRequired(false); // Apenas sigla (SP, RJ)
    }
}
