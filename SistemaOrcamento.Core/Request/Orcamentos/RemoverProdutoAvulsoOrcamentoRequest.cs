using System.ComponentModel.DataAnnotations;

namespace SistemaOrcamento.Core.Request.Orcamentos;

public class RemoverProdutoAvulsoOrcamentoRequest
{
    [Required (ErrorMessage = "O campo {0} é obrigatório")]
    public int Id { get; set; }
    [Required (ErrorMessage = "O campo {0} é obrigatório")]
    public int OrcamentoId { get; set; }
}