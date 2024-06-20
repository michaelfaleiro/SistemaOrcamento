namespace SistemaOrcamento.Core.Models;

public class Cotacao
{
    public int Id { get; set; }
    public Orcamento Orcamento { get; set; } = new();
    public IList<CotacaoProduto> CotacaoProdutos { get; set; } = new List<CotacaoProduto>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
}