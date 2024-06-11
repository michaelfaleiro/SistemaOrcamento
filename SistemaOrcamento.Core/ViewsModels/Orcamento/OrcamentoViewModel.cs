using SistemaOrcamento.Core.ViewsModels.Cliente;
using SistemaOrcamento.Core.ViewsModels.Veiculo;

namespace SistemaOrcamento.Core.ViewsModels.Orcamento;

public class OrcamentoViewModel
{
    public int Id { get; set; }
    public ClienteViewModel Cliente { get; set; }
    public VeiculoViewModel Veiculo { get; set; }
    public IEnumerable<ProdutoOrcamentoViewModel> ItensOrcamento { get; set; } = new List<ProdutoOrcamentoViewModel>();
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}