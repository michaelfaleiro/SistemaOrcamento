namespace SistemaOrcamento.Core.ViewsModels.Cotacao;

public class CotacaoViewModel
{
    public int Id { get; set; }
    public IEnumerable<CotacaoProdutoViewModel> CotacaoProdutos { get; set; } = new List<CotacaoProdutoViewModel>();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}