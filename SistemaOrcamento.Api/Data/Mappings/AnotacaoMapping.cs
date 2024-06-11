using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaOrcamento.Core.Models;

namespace SistemaOrcamento.Api.Data.Mappings;

public class AnotacaoMapping : IEntityTypeConfiguration<Anotacao>
{
    public void Configure(EntityTypeBuilder<Anotacao> builder)
    {
        builder.ToTable("Anotacoes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn()
            .HasColumnType("int");

        builder.Property(x => x.Descricao)
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();
        
    }
}