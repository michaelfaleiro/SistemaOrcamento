namespace SistemaOrcamento.Core.ViewsModels.Veiculo;

public class VeiculoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Placa { get; set; }
    public string Chassi { get; set; }
    public int? Ano { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}