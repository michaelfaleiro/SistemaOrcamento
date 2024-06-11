using System.ComponentModel.DataAnnotations;

namespace SistemaOrcamento.Core.Request.Veiculos;

public class GetVeiculoByIdRequest : Request
{
    [Required (ErrorMessage = "O campo {0} é obrigatório.")]
    public int Id { get; set; }
}