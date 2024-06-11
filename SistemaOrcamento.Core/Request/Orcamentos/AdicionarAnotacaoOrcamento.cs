using System.ComponentModel.DataAnnotations;
using SistemaOrcamento.Core.Models;

namespace SistemaOrcamento.Core.Request.Orcamentos;

public class AdicionarAnotacaoOrcamento
{
    [Required (ErrorMessage = "O campo Anotacao é obrigatório")]
    public int OrcamentoId { get; set; }
    [Required (ErrorMessage = "O campo Anotacao é obrigatório")]
    public Anotacao Anotacao { get; set; }
}