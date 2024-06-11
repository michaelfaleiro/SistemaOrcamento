namespace SistemaOrcamento.Core.Models;

public class Veiculo
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Placa { get; set; } = string.Empty;
    public string Chassi { get; set; } = string.Empty;
    public int? Ano { get; set; }
    public ICollection<ClienteVeiculo> ClienteVeiculos { get; set; } = new List<ClienteVeiculo>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
    
