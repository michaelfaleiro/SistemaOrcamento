using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaOrcamento.Core.Models;

namespace SistemaOrcamento.Api.Data.Mappings;

public class ClienteMapping : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);

        builder.Property(x => x.Telefone)
            .IsRequired()
            .HasColumnName("Telefone")
            .HasColumnType("VARCHAR")
            .HasMaxLength(12);
       
        builder.Property(x => x.CreatedAt)
            .IsRequired();
        
        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.HasMany<Orcamento>(x => x.Orcamentos)
            .WithOne(x => x.Cliente)
            .HasForeignKey(x => x.Id);
        
        builder.HasMany(x => x.ClienteVeiculos)
            .WithOne(x => x.Cliente)
            .HasForeignKey(x => x.ClienteId);

    }
}