namespace SistemaOrcamento.Core.Models;

public class CotacaoProduto
{
    public int Id { get; set; }
    public Cotacao Cotacao { get; set; } = new();
    public string Nome { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public IList<CotacaoPrecoProduto> PrecoProdutos { get; set; } = new List<CotacaoPrecoProduto>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}