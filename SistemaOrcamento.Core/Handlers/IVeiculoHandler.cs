using SistemaOrcamento.Core.Models;
using SistemaOrcamento.Core.Request.Veiculos;
using SistemaOrcamento.Core.Response;

namespace SistemaOrcamento.Core.Handlers;

public interface IVeiculoHandler
{
    Task<Response<Veiculo?>> CreateAsync(CreateVeiculoRequest request);
    Task<Response<Veiculo?>> UpdateAsync(UpdateVeiculoRequest request);
    Task<Response<Veiculo?>> DeleteAsync(DeleteVeiculoRequest request);
    Task<Response<Veiculo?>> GetByIdAsync(GetVeiculoByIdRequest request);
    Task<PagedResponse<IEnumerable<Veiculo>?>> GetAllAsync(GetAllVeiculoRequest request);
}