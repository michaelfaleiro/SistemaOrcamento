namespace SistemaOrcamento.Core.Request.Cotacao;

public class RemoverProdutoCotacaoRequest : Request
{
    public int CotacaoId { get; set; }
    public int ProdutoId { get; set; }
}