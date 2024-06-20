using SistemaOrcamento.Core.Models;
using SistemaOrcamento.Core.Request.Cotacao;
using SistemaOrcamento.Core.Response;
using SistemaOrcamento.Core.ViewsModels.Cotacao;

namespace SistemaOrcamento.Core.Handlers;

public interface ICotacaoHandler
{
    Task<Response<Cotacao?>> CreateAsync(CreateCotacaoRequest request);
    Task<Response<Cotacao?>> DeleteAsync(DeleteCotacaoRequest request);
    Task<Response<CotacaoViewModel?>> GetByIdAsync(GetCotacaoByIdRequest request);
    Task<PagedResponse<IEnumerable<CotacaoViewModel>?>> GetAllAsync(GetAllCotacaoRequest request);
    Task<Response<Cotacao?>> AdicionarProdutoCotacaoAsync(AdicionarProdutoCotacaoRequest request);
    Task<Response<Cotacao?>> UpdateProdutoCotacoAsync(UpdateProdutoCotacaoRequest request);
    Task<Response<Cotacao?>> RemoverProdutoCotacaoAsync(RemoverProdutoCotacaoRequest request);
}