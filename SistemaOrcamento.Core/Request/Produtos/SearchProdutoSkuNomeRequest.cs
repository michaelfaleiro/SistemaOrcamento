namespace SistemaOrcamento.Core.Request.Produtos;

public class SearchProdutoSkuNomeRequest : PagedRequest
{
    public string Query { get; set; }
}