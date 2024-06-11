using SistemaOrcamento.Core.Models;
using SistemaOrcamento.Core.Request.Produtos;
using SistemaOrcamento.Core.Response;

namespace SistemaOrcamento.Core.Handlers;

public interface IProdutoHandler
{
    Task<Response<Produto?>> CreateAsync(CreateProdutoRequest request);
    Task<Response<Produto?>> UpdateAsync(UpdateProdutoRequest request);
    Task<Response<Produto?>> DeleteAsync(DeleteProdutoRequest request);
    Task<Response<Produto?>> GetByIdAsync(GetProdutoByIdRequest request);
    Task<PagedResponse<IEnumerable<Produto>?>> GetAllAsync(GetAllProdutoRequest request);
    Task<PagedResponse<IEnumerable<Produto>?>> SearchBySkuNomeAsync(SearchProdutoSkuNomeRequest request);
}