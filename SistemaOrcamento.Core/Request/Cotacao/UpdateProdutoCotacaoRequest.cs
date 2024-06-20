using System.ComponentModel.DataAnnotations;

namespace SistemaOrcamento.Core.Request.Cotacao;

public class UpdateProdutoCotacaoRequest : Request
{
    [Required (ErrorMessage = "O campo {0} é obrigatório")]
    public int CotacaoId { get; set; }
    [Required (ErrorMessage = "O campo {0} é obrigatório")]
    public int ProdutoId { get; set; }
    [Required (ErrorMessage = "O campo {0} é obrigatório")]
    public string Nome { get; set; } = null!;
    [Required (ErrorMessage = "O campo {0} é obrigatório")]
    public string Sku { get; set; } = null!;
}