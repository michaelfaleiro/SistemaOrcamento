namespace SistemaOrcamento.Core.Models;

public class CotacaoPrecoProduto
{
    public int Id { get; set; }
    public CotacaoProduto CotacaoProduto { get; set; } = new();
    public decimal ValorCusto { get; set; }
    public decimal ValorVenda { get; set; }
    public Fornecedor Fornecedor { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}