using System.ComponentModel.DataAnnotations;

namespace SistemaOrcamento.Core.Request.Produtos;

public class CreateProdutoRequest : Request
{
    [Required (ErrorMessage = "O campo SKU é obrigatório.")]
    public string Sku { get; set; } = string.Empty;
    [Required (ErrorMessage = "O campo Nome é obrigatório.")]
    public string Nome { get; set; } = string.Empty;
    [Required (ErrorMessage = "O campo Valor de Venda é obrigatório.")]
    public string Fabricante { get; set; } = string.Empty;
    [Required (ErrorMessage = "O campo Valor de Venda é obrigatório.")]
    public decimal ValorVenda { get; set; } 
}