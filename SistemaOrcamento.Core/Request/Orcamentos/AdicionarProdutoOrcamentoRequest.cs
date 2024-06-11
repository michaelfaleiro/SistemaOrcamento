using System.ComponentModel.DataAnnotations;

namespace SistemaOrcamento.Core.Request.Orcamentos;

public class AdicionarProdutoOrcamentoRequest : Request
{
    [Required (ErrorMessage = "O campo OrcamentoId é obrigatório")]
    public int OrcamentoId { get; set; }
    [Required (ErrorMessage = "O campo ProdutoId é obrigatório")]
    public int ProdutoId { get; set; }
    [Required (ErrorMessage = "O campo Quantidade é obrigatório")]
    public int Quantidade { get; set; }
    [Required (ErrorMessage = "O campo ValorVenda é obrigatório")]
    public decimal ValorVenda { get; set; }
}