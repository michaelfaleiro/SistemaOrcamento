namespace SistemaOrcamento.Core.ViewsModels.Orcamento;

public class ProdutoOrcamentoViewModel
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public string Sku { get; set; }
    public string Nome { get; set; }
    public string Fabricante { get; set; }
    public int  Quantidade { get; set; }
    public decimal ValorVenda { get; set; }
}