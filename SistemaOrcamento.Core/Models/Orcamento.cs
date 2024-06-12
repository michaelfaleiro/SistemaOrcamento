namespace SistemaOrcamento.Core.Models;

public class Orcamento
{
    public int Id { get; set; }
    public Cliente Cliente { get; set; } = null!;
    public Veiculo Veiculo { get; set; } = null!;
    public IList<OrcamentoProduto> OrcamentoProdutos { get; set; } = [];
    public IList<ProdutoAvulso> ProdutoAvulsos { get; set; } = [];
    public string Status { get; set; } = string.Empty;
    public IList<Anotacao> Anotacoes { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}