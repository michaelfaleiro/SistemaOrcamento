using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaOrcamento.Core.Models;

namespace SistemaOrcamento.Api.Data.Mappings;

public class VeiculoMapping :IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.ToTable("Veiculos");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(40);
        
        builder.Property(x => x.Placa)
            .IsRequired()
            .HasColumnName("Placa")
            .HasColumnType("VARCHAR")
            .HasMaxLength(8);
        
        
        builder.Property(x => x.Chassi)
            .IsRequired()
            .HasColumnName("Chassi")
            .HasColumnType("VARCHAR")
            .HasMaxLength(17);
        
        builder.Property(x => x.Ano)
            .IsRequired()
            .HasColumnName("Ano")
            .HasColumnType("INT");
        
        builder.Property(x => x.CreatedAt)
            .IsRequired();
        
        builder.Property(x => x.UpdatedAt)
            .IsRequired();
        
        builder.HasMany(x => x.ClienteVeiculos)
            .WithOne(x => x.Veiculo)
            .HasForeignKey(x => x.VeiculoId);
    }
}