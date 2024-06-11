using System.ComponentModel.DataAnnotations;

namespace SistemaOrcamento.Core.Request.Orcamentos;

public class CreateOrcamentoRequest : Request
{
    [Required (ErrorMessage = "O campo Nome é obrigatório")]
    public int ClienteId { get; set; }
    [Required (ErrorMessage = "O campo Nome é obrigatório")]
    public int VeiculoId { get; set; }
    public string Status { get; set; } = string.Empty;
}