using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaOrcamento.Core.Models;

namespace SistemaOrcamento.Api.Data.Mappings;

public class OrcamentProdutoMapping : IEntityTypeConfiguration<OrcamentoProduto>
{
    public void Configure(EntityTypeBuilder<OrcamentoProduto> builder)
    {
        builder.HasKey(x => new { x.OrcamentoId, x.ProdutoId });

        builder.HasOne(x => x.Orcamento)
            .WithMany(x => x.OrcamentoProdutos)
            .HasForeignKey(x => x.OrcamentoId);

        builder.HasOne(x => x.Produto)
            .WithMany(x => x.OrcamentoProdutos)
            .HasForeignKey(x => x.ProdutoId);
    }
}
