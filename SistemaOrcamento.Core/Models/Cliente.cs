namespace SistemaOrcamento.Core.Models;

public class Cliente
{
    public int Id { get; set; } 
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public IList<Orcamento> Orcamentos { get; set; } = new List<Orcamento>();
    public IList<ClienteVeiculo> ClienteVeiculos { get; set; } = new List<ClienteVeiculo>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}