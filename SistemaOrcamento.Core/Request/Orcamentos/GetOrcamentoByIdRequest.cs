using System.ComponentModel.DataAnnotations;

namespace SistemaOrcamento.Core.Request.Orcamentos;

public class GetOrcamentoByIdRequest : Request
{
    [Required (ErrorMessage = "O Id do orçamento é obrigatório")]
    public int Id { get; set; }
}