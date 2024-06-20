namespace SistemaOrcamento.Core.ViewsModels.Orcamento;

public class ProdutoAvulsoViewModel
{
    public int Id { get; set; }
    public string Sku { get; set; }
    public string Nome { get; set; }
    public int Quantidade { get; set; }
    public string Fabricante { get; set; }
    public decimal ValorVenda { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}