using Microsoft.EntityFrameworkCore;
using SistemaOrcamento.Api.Data.Mappings;
using SistemaOrcamento.Core.Models;

namespace SistemaOrcamento.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes { get; set; } = null!;
    public DbSet<Veiculo> Veiculos { get; set; } = null!;
    public DbSet<ClienteVeiculo> ClienteVeiculos { get; set; } = null!;
    public DbSet<Orcamento> Orcamentos { get; set; } = null!;
    public DbSet<OrcamentoProduto> OrcamentoProdutos { get; set; } = null!;
    public DbSet<Anotacao> Anotacoes { get; set; } = null!;
    public DbSet<Produto> Produtos { get; set; } = null!;
    
}