using System.ComponentModel.DataAnnotations;

namespace SistemaOrcamento.Core.Request.Veiculos;

public class UpdateVeiculoRequest
{
    [Required (ErrorMessage = "O campo {0} é obrigatório.")]
    public int Id { get; set; }
    [Required (ErrorMessage = "O campo {0} é obrigatório.")]
    public string Nome { get; set; } = string.Empty;
    public string Placa { get; set; } = string.Empty;  
    [Required (ErrorMessage = "O campo {0} é obrigatório.")]
    public string Chassi { get; set; } = string.Empty;
    public int? Ano { get; set; }
}