using System.ComponentModel.DataAnnotations;

namespace SistemaOrcamento.Core.Request.Cotacao;

public class AdicionarProdutoCotacaoRequest : Request
{
    [Required (ErrorMessage = "O campo {0} é obrigatório")]
    public int CotacaoId { get; set; }
    [Required (ErrorMessage = "O campo {0} é obrigatório")]
    public string Nome { get; set; } = null!;

    public string Sku { get; set; } = string.Empty;
}