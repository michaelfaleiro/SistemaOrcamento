using System.ComponentModel.DataAnnotations;

namespace SistemaOrcamento.Core.Request.Clientes;

public class AdicionarVeiculoClienteRequest : Request
{
    [Required (ErrorMessage = "O campo {0} é obrigatório.")]
    public int ClienteId { get; set; }
    [Required (ErrorMessage = "O campo {0} é obrigatório.")]
    public int VeiculoId { get; set; }
}