namespace SistemaOrcamento.Core.Models;

public class ProdutoAvulso
{
    public int Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Fabricante { get; set; } = string.Empty;
    public int Quantidade { get; set; } 
    public decimal ValorVenda { get; set; }
    public Orcamento Orcamento { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}