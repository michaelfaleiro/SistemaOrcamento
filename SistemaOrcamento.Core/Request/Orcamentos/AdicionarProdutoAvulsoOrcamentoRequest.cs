using System.ComponentModel.DataAnnotations;

namespace SistemaOrcamento.Core.Request.Orcamentos;
public class AdicionarProdutoAvulsoOrcamentoRequest
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int OrcamentoId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Sku { get; set; } = null!;
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Nome { get; set; } = null!;
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int Quantidade { get; set; }
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Fabricante { get; set; } = null!;
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public decimal ValorVenda { get; set; }
}
