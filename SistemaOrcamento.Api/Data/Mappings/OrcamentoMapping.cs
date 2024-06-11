using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaOrcamento.Core.Models;

namespace SistemaOrcamento.Api.Data.Mappings;

public class OrcamentoMapping : IEntityTypeConfiguration<Orcamento>
{
    public void Configure(EntityTypeBuilder<Orcamento> builder)
    {
        builder.ToTable("Orcamentos");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(o => o.UpdatedAt)
            .HasColumnType("datetime")
            .IsRequired();

        builder.HasOne(o => o.Cliente)
            .WithMany(c => c.Orcamentos) // Aqui você deve referenciar a coleção de Orcamentos na entidade Cliente
            .HasForeignKey(o => o.Id);

        builder.HasMany(o => o.OrcamentoProdutos)
            .WithOne(op => op.Orcamento)
            .HasForeignKey(op => op.OrcamentoId);

        builder.HasMany(x => x.Anotacoes);
    }
}