using SistemaOrcamento.Core.ViewsModels.Cliente;
using SistemaOrcamento.Core.Models;
using SistemaOrcamento.Core.Request.Clientes;
using SistemaOrcamento.Core.Response;

namespace SistemaOrcamento.Core.Handlers;

public interface IClienteHandler
{
    Task<Response<Cliente?>> CreateAsync(CreateClienteRequest request);
    Task<Response<Cliente?>> UpdateAsync(UpdateClienteRequest request);
    Task<Response<Cliente?>> DeleteAsync(DeleteClienteRequest request);
    Task<Response<Cliente?>> GetByIdAsync(GetClienteByIdRequest request);
    Task<Response<ClienteComVeiculoViewModel?>> GetByIdComVeiculoAsync(GetClienteByIdRequest request);
    Task<Response<IEnumerable<ClienteComVeiculoViewModel>?>> GetByNomeTelefonePlacaAsync(GetClienteByNomeTelefonePlacaRequest request);
    Task<Response<Cliente?>> AdicionarVeiculoAsync(AdicionarVeiculoClienteRequest request);
    Task<PagedResponse<IEnumerable<Cliente>?>> GetAllAsync(GetAllClienteRequest request);
}