namespace SistemaOrcamento.Core.Models;

public class ClienteVeiculo
{
    public int Id { get; set; }
    public int ClienteId { get; set; } 
    public Cliente Cliente { get; set; } = null!;
    
    public int VeiculoId { get; set; }
    public Veiculo Veiculo { get; set; } = null!;
}