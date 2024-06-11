namespace SistemaOrcamento.Core.Models;

public class OrcamentoProduto
{
    public int Id { get; set; }
    public int OrcamentoId { get; set; }
    public Orcamento Orcamento { get; set; } = null!;
    
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;
    
    public int Quantidade { get; set; }
    public decimal ValorVenda { get; set; }
}