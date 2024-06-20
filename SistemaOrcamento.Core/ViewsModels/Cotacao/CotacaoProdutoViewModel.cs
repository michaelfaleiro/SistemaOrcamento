namespace SistemaOrcamento.Core.ViewsModels.Cotacao;

public class CotacaoProdutoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string? Sku { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}