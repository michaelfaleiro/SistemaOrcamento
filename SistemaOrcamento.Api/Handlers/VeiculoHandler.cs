using Microsoft.EntityFrameworkCore;
using SistemaOrcamento.Api.Data;
using SistemaOrcamento.Core.Handlers;
using SistemaOrcamento.Core.Models;
using SistemaOrcamento.Core.Request.Veiculos;
using SistemaOrcamento.Core.Response;

namespace SistemaOrcamento.Api.Handlers;

public class VeiculoHandler(AppDbContext context) : IVeiculoHandler
{
    public async Task<Response<Veiculo?>> CreateAsync(CreateVeiculoRequest request)
    {
        var veiculo = new Veiculo
        {
            Nome = request.Nome,
            Placa = request.Placa,
            Chassi = request.Chassi,
            Ano = request.Ano,
        };
        try
        {
            await context.Veiculos.AddAsync(veiculo);
            await context.SaveChangesAsync();

            return new Response<Veiculo?>(veiculo, 201, message: "Veículo criado com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Veiculo?>(null, 500, message: "Não foi possível criar o veículo");
            
        }
        catch (Exception)
        {
            return new Response<Veiculo?>(null, 500, message: "Falha interna no servidor");
        }
    }

    public async Task<Response<Veiculo?>> UpdateAsync(UpdateVeiculoRequest request)
    {
        try
        {
            var veiculo = await context.Veiculos.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (veiculo is null)
                return new Response<Veiculo?>(null, 404, message: "Veículo não encontrado");

            veiculo.Nome = request.Nome;
            veiculo.Placa = request.Placa;
            veiculo.Chassi = request.Chassi;
            veiculo.Ano = request.Ano;
            veiculo.UpdatedAt = DateTime.UtcNow;

            context.Veiculos.Update(veiculo);
            await context.SaveChangesAsync();

            return new Response<Veiculo?>(veiculo, message: "Veículo atualizado com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Veiculo?>(null, 500, message: "Não foi possível atualizar o veículo");
        }
        catch (Exception)
        {
            return new Response<Veiculo?>(null, 500, message: "Falha interna no servidor");
        }
    }

    public async Task<Response<Veiculo?>> DeleteAsync(DeleteVeiculoRequest request)
    {
        try
        {
            var veiculo = await context.Veiculos.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (veiculo is null)
                return new Response<Veiculo?>(null, 404, message: "Veículo não encontrado");

            context.Veiculos.Remove(veiculo);
            await context.SaveChangesAsync();

            return new Response<Veiculo?>(veiculo, message: "Veículo removido com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Veiculo?>(null, 500, message: "Não foi possível remover o veículo");
        }
        catch (Exception)
        {
            return new Response<Veiculo?>(null, 500, message: "Falha interna no servidor");
        }
    }

    public async Task<Response<Veiculo?>> GetByIdAsync(GetVeiculoByIdRequest request)
    {
        try
        {
            var veiculo = await context.Veiculos.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (veiculo is null)
                return new Response<Veiculo?>(null, 404, message: "Veículo não encontrado");

            return new Response<Veiculo?>(veiculo);
        }
        catch (DbUpdateException)
        {
            return new Response<Veiculo?>(null, 500, message: "Não foi possível buscar o veiculo");
        }
        catch (Exception)
        {
            return new Response<Veiculo?>(null, 500, message: "Falha interna no servidor");
        }
    }

    public async Task<PagedResponse<IEnumerable<Veiculo>?>> GetAllAsync(GetAllVeiculoRequest request)
    {
        try
        {
            var query = context.Veiculos.AsNoTracking();
            
            var veiculos = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();
            
            return new PagedResponse<IEnumerable<Veiculo>?>(
                veiculos,
                count,
                request.PageNumber,
                request.PageSize
                );
        }
        catch (DbUpdateException)
        {
            return new PagedResponse<IEnumerable<Veiculo>?>(null, 500, message: "Não foi possível buscar os veiculos");
        }
        catch (Exception)
        {
            return new PagedResponse<IEnumerable<Veiculo>?>(null, 500, message: "Falha interna no servidor");
        }
    }
}