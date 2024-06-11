using SistemaOrcamento.Core.ViewsModels.Veiculo;

namespace SistemaOrcamento.Core.ViewsModels.Cliente;

public class ClienteComVeiculoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public List<VeiculoViewModel> Veiculos { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}