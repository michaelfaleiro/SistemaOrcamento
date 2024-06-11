using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaOrcamento.Core.Models;

namespace SistemaOrcamento.Api.Data.Mappings;

public class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Sku)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.ValorVenda)
            .HasColumnType("MONEY")
            .IsRequired();

        builder.HasMany(p => p.OrcamentoProdutos)
            .WithOne(op => op.Produto)
            .HasForeignKey(op => op.ProdutoId);

        builder.Property(p => p.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasColumnType("datetime")
            .IsRequired();
    }
}