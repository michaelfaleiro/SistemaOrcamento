using System.ComponentModel.DataAnnotations;

namespace SistemaOrcamento.Core.Request.Clientes;

public class GetClienteByIdRequest : Request
{
    [Required (ErrorMessage = "O campo {0} é obrigatório")]
    public int Id { get; set; }
}