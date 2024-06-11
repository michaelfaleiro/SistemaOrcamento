using Microsoft.EntityFrameworkCore;
using SistemaOrcamento.Api.Data;
using SistemaOrcamento.Core.Handlers;
using SistemaOrcamento.Core.Models;
using SistemaOrcamento.Core.Request.Clientes;
using SistemaOrcamento.Core.Response;
using SistemaOrcamento.Core.ViewsModels.Cliente;
using SistemaOrcamento.Core.ViewsModels.Veiculo;

namespace SistemaOrcamento.Api.Handlers;

public class ClienteHandler(AppDbContext context) : IClienteHandler
{
    public async Task<Response<Cliente?>> CreateAsync(CreateClienteRequest request)
    {
        var cliente = new Cliente()
        {
            Nome = request.Nome,
            Telefone = request.Telefone
        };

        try
        {
            await context.Clientes.AddAsync(cliente);
            await context.SaveChangesAsync();

            return new Response<Cliente?>(cliente, 201, message: "Cliente criado com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Cliente?>(null, 500, message: "Não foi possível criar o cliente");
        }
        catch (Exception)
        {
            return new Response<Cliente?>(null, 500, message: "Falha interna no servidor");
        }

    }

    public async Task<Response<Cliente?>> UpdateAsync(UpdateClienteRequest request)
    {
        try
        {
            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (cliente is null)
                return new Response<Cliente?>(null, 404, message: "Cliente não encontrado");

            cliente.Nome = request.Nome;
            cliente.Telefone = request.Telefone;
            cliente.UpdatedAt = DateTime.UtcNow;

            context.Clientes.Update(cliente);
            await context.SaveChangesAsync();

            return new Response<Cliente?>(cliente, message: "Cliente atualizado com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Cliente?>(null, 500, message: "Não foi possível atualizar o cliente");
        }
        catch (Exception)
        {
            return new Response<Cliente?>(null, 500, message: "Falha interna no servidor");
        }
    }

    public async Task<Response<Cliente?>> DeleteAsync(DeleteClienteRequest request)
    {
        try
        {
            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (cliente is null)
                return new Response<Cliente?>(null, 404, message: "Cliente não encontrado");

            context.Clientes.Remove(cliente);
            await context.SaveChangesAsync();

            return new Response<Cliente?>(null, message: "Cliente deletado com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Cliente?>(null, 500, message: "Não foi possível excluir o cliente");
        }
        catch (Exception)
        {
            return new Response<Cliente?>(null, 500, message: "Falha interna no servidor");
        }
    }

    public async Task<Response<Cliente?>> GetByIdAsync(GetClienteByIdRequest request)
    {
        try
        {
            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == request.Id);

            return cliente is null
                ? new Response<Cliente?>(null, 404, message: "Cliente não encontrado") :
                new Response<Cliente?>(cliente);
        }
        catch (DbUpdateException)
        {
            return new Response<Cliente?>(null, 500, message: "Não foi possível buscar o cliente");
        }
        catch (Exception)
        {
            return new Response<Cliente?>(null, 500, message: "Falha interna no servidor");
        }
    }

    public async Task<Response<ClienteComVeiculoViewModel?>> GetByIdComVeiculoAsync(GetClienteByIdRequest request)
    {
        try
        {
            var cliente = await context.Clientes
                .Include(x => x.ClienteVeiculos)
                .ThenInclude(x => x.Veiculo)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if(cliente is null)
                return new Response<ClienteComVeiculoViewModel?>(null, 404, message: "Cliente não encontrado");
            
            var veiculos = cliente.ClienteVeiculos.Select(cv => new VeiculoViewModel()
            {
                Id = cv.Veiculo.Id,
                Nome = cv.Veiculo.Nome,
                Placa = cv.Veiculo.Placa,
                Ano = cv.Veiculo.Ano,
                CreatedAt = cv.Veiculo.CreatedAt,
                UpdatedAt = cv.Veiculo.UpdatedAt
            }).ToList(); 
            
            var clienteComVeiculo = new ClienteComVeiculoViewModel()
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                Veiculos = veiculos,
                CreatedAt = cliente.CreatedAt,
                UpdatedAt = cliente.UpdatedAt
            };
            
            return new Response<ClienteComVeiculoViewModel?>(clienteComVeiculo);
        }
        catch (DbUpdateException)
        {
            return new Response<ClienteComVeiculoViewModel?>(null, 500, message: "Não foi possível buscar o cliente");
        }
        catch (Exception)
        {
            return new Response<ClienteComVeiculoViewModel?>(null, 500, message: "Falha interna no servidor");
        }
    }

    public async Task<Response<IEnumerable<ClienteComVeiculoViewModel>?>> GetByNomeTelefonePlacaAsync(
        GetClienteByNomeTelefonePlacaRequest request)
    {
        try
        {
            var query = context.Clientes
                .AsNoTracking()
                .Include(x => x.ClienteVeiculos)
                .ThenInclude(x => x.Veiculo)
                .Where(c => c.Nome.Contains(request.Query)
                            || c.Telefone.Contains(request.Query)
                            || c.ClienteVeiculos.Any(cv =>
                                cv.Veiculo.Placa.Contains(request.Query)));
            
            var clientes = await query
                .ToListAsync();
            
            var result = clientes.Select(cliente => new ClienteComVeiculoViewModel
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                Veiculos = cliente.ClienteVeiculos.Select(cv => new VeiculoViewModel
                {
                    Id = cv.Veiculo.Id,
                    Nome = cv.Veiculo.Nome,
                    Placa = cv.Veiculo.Placa,
                    Chassi = cv.Veiculo.Chassi,
                    Ano = cv.Veiculo.Ano,
                    CreatedAt = cv.Veiculo.CreatedAt,
                    UpdatedAt = cv.Veiculo.UpdatedAt
                }).ToList(),
                CreatedAt = cliente.CreatedAt,
                UpdatedAt = cliente.UpdatedAt
            }).ToList();
            
            return new Response<IEnumerable<ClienteComVeiculoViewModel>?>(result);
        }
        catch (DbUpdateException)
        {
            return new Response<IEnumerable<ClienteComVeiculoViewModel>?>(null, 500, message: "Não foi possível buscar os cliente");
        }
        catch (Exception)
        {
            return new Response<IEnumerable<ClienteComVeiculoViewModel>?>(null, 500, message: "Falha interna no servidor");
        }
    }

    public async Task<PagedResponse<IEnumerable<Cliente>?>> GetAllAsync(GetAllClienteRequest request)
    {
        try
        {
            var query = context
                .Clientes
                .AsNoTracking()
                .OrderBy(x => x.Nome);

            var clientes = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<IEnumerable<Cliente>?>(
                clientes,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch (DbUpdateException)
        {
            return new PagedResponse<IEnumerable<Cliente>?>(null, 500, message: "Não foi possível buscar os clientes");
        }
        catch (Exception)
        {
            return new PagedResponse<IEnumerable<Cliente>?>(null, 500, message: "Falha interna no servidor");
        }

    }

    public async Task<Response<Cliente?>> AdicionarVeiculoAsync(AdicionarVeiculoClienteRequest request)
        {
            try
            {
                var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == request.ClienteId);
                if (cliente is null)
                    return new Response<Cliente?>(null, 404, message: "Cliente não encontrado");

                var veiculo = await context.Veiculos.FirstOrDefaultAsync(x => x.Id == request.VeiculoId);
                if (veiculo is null)
                    return new Response<Cliente?>(null, 404, message: "Veículo não encontrado");

                cliente.ClienteVeiculos.Add(new ClienteVeiculo()
                {
                    Cliente = cliente,
                    Veiculo = veiculo
                });
                
                context.Clientes.Update(cliente);
                await context.SaveChangesAsync();

                return new Response<Cliente?>(cliente, message: "Veículo adicionado ao cliente com sucesso");
            }
            catch (DbUpdateException)
            {
                return new Response<Cliente?>(null, 500, message: "Não foi possível adicionar o veículo ao cliente");
            }
            catch (Exception)
            {
                return new Response<Cliente?>(null, 500, message: "Falha interna no servidor");
            }
        }
    
}