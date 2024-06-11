using SistemaOrcamento.Core.Models;
using SistemaOrcamento.Core.Request.Orcamentos;
using SistemaOrcamento.Core.Response;
using SistemaOrcamento.Core.ViewsModels.Orcamento;

namespace SistemaOrcamento.Core.Handlers;

public interface IOrcamentoHandler
{
    Task<Response<Orcamento?>> CreateAsync(CreateOrcamentoRequest request);
    Task<Response<Orcamento?>> UpdateAsync(UpdateOrcamentoRequest request);
    Task<Response<Orcamento?>> DeleteAsync(DeleteOrcamentoRequest request);
    Task<Response<OrcamentoViewModel?>> GetByIdAsync(GetOrcamentoByIdRequest request);
    Task<PagedResponse<IEnumerable<OrcamentoViewModel>?>> GetAllAsync(GetAllOrcamentoRequest request);
    Task<Response<Orcamento?>> AdicionarProdutoOrcamentoAsync(AdicionarProdutoOrcamentoRequest request);
    Task<Response<Orcamento?>> UpdateProdutoOrcamentoAsync(UpdateProdutoOrcamentoRequest request);
    Task<Response<Orcamento?>> RemoverProdutoOrcamentoAsync(RemoverProdutoOrcamentoRequest request);
}