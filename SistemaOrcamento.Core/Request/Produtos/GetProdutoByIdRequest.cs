using System.ComponentModel.DataAnnotations;

namespace SistemaOrcamento.Core.Request.Produtos;

public class GetProdutoByIdRequest : Request
{
    [Required (ErrorMessage = "O campo Id é obrigatório.")]
    public int Id { get; set; }
}