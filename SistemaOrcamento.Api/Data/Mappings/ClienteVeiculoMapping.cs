using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaOrcamento.Core.Models;

namespace SistemaOrcamento.Api.Data.Mappings;

public class ClienteVeiculoMapping : IEntityTypeConfiguration<ClienteVeiculo>
{
    public void Configure(EntityTypeBuilder<ClienteVeiculo> builder)
    {
        builder.ToTable("ClienteVeiculos");
        
        builder.HasKey(x => new { x.ClienteId, x.VeiculoId });
        
        builder.HasOne(x => x.Cliente)
            .WithMany(x => x.ClienteVeiculos)
            .HasForeignKey(x => x.ClienteId);
        
        builder.HasOne(x => x.Veiculo)
            .WithMany(x => x.ClienteVeiculos)
            .HasForeignKey(x => x.VeiculoId);
    }
}