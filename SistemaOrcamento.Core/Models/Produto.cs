namespace SistemaOrcamento.Core.Models;

public class Produto
{
    public int Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Fabricante { get; set; } = string.Empty;
    public decimal ValorVenda { get; set; }
    public IList<OrcamentoProduto> OrcamentoProdutos { get; set; } = new List<OrcamentoProduto>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}