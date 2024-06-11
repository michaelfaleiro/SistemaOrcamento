using System.ComponentModel.DataAnnotations;

namespace SistemaOrcamento.Core.Request.Clientes;

public class CreateClienteRequest : Request
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Nome { get; set; } = string.Empty;
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Telefone { get; set; } = string.Empty;
}