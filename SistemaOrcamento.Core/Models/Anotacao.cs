namespace SistemaOrcamento.Core.Models;

public class Anotacao
{
    public int Id { get; set; }
    public string Descricao { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}