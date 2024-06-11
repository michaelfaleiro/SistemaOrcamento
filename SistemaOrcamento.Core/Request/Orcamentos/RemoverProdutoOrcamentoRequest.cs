using System.ComponentModel.DataAnnotations;

namespace SistemaOrcamento.Core.Request.Orcamentos;

public class RemoverProdutoOrcamentoRequest
{
    [Required (ErrorMessage = "O campo Id é obrigatório")]
    public int Id { get; set; }
    [Required (ErrorMessage = "O campo OrcamentoId é obrigatório")]
    public int OrcamentoId { get; set; }
    [Required (ErrorMessage = "O campo ProdutoId é obrigatório")]
    public int ProdutoId { get; set; }
}