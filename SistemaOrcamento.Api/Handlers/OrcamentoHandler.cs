using Microsoft.EntityFrameworkCore;
using SistemaOrcamento.Api.Data;
using SistemaOrcamento.Core.Handlers;
using SistemaOrcamento.Core.Models;
using SistemaOrcamento.Core.Request.Orcamentos;
using SistemaOrcamento.Core.Response;
using SistemaOrcamento.Core.ViewsModels.Cliente;
using SistemaOrcamento.Core.ViewsModels.Orcamento;
using SistemaOrcamento.Core.ViewsModels.Veiculo;

namespace SistemaOrcamento.Api.Handlers;

public class OrcamentoHandler(AppDbContext context) : IOrcamentoHandler
{
    public async Task<Response<Orcamento?>> CreateAsync(CreateOrcamentoRequest request)
    {
        var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == request.ClienteId);
        if (cliente is null)
            return new Response<Orcamento?>(null, 404, message: "Cliente não encontrado");
        
        var veiculo = await context.Veiculos.FirstOrDefaultAsync(x => x.Id == request.VeiculoId);
        if(veiculo is null)
            return new Response<Orcamento?>(null, 404, message: "Veículo não encontrado");

        var orcamento = new Orcamento()
        {
            Cliente = cliente,
            Veiculo = veiculo,
            Status = request.Status
        };
        
        try
        {
            await context.Orcamentos.AddAsync(orcamento);
            await context.SaveChangesAsync();

            return new Response<Orcamento?>(orcamento, 201, message: "Orçamento criado com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Orcamento?>(null, 500, message: "Não foi possível criar o orçamento");
        }
        catch (Exception)
        {
            return new Response<Orcamento?>(null, 500, message: "Falha interna no servidor");
        }

    }

    public async Task<Response<Orcamento?>> UpdateAsync(UpdateOrcamentoRequest request)
    {
        try
        {
            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == request.ClienteId);
            if (cliente is null)
                return new Response<Orcamento?>(null, 404, message: "Cliente não encontrado");
        
            var veiculo = await context.Veiculos.FirstOrDefaultAsync(x => x.Id == request.VeiculoId);
            if(veiculo is null)
                return new Response<Orcamento?>(null, 404, message: "Veículo não encontrado");
        
            var orcamento = await context.Orcamentos
                .Include(x => x.Cliente)
                .Include(x => x.Veiculo)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (orcamento is null)
                return new Response<Orcamento?>(null, 404, message: "Orçamento não encontrado");
        
            orcamento.Cliente = cliente;
            orcamento.Veiculo = veiculo;
            orcamento.Status = request.Status;
            orcamento.UpdatedAt = DateTime.UtcNow;
            
            context.Orcamentos.Update(orcamento);
            await context.SaveChangesAsync();

            return new Response<Orcamento?>(orcamento, message: "Orçamento atualizado com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Orcamento?>(null, 500, message: "Não foi possível atualizar o orçamento");
        }
        catch (Exception)
        {
            return new Response<Orcamento?>(null, 500, message: "Falha interna no servidor");
        }
    }

    public async Task<Response<Orcamento?>> DeleteAsync(DeleteOrcamentoRequest request)
    {
        try
        {
            var orcamento = await context.Orcamentos.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (orcamento is null)
                return new Response<Orcamento?>(null, 404, message: "Orçamento não encontrado");
            
            context.Orcamentos.Remove(orcamento);
            await context.SaveChangesAsync();

            return new Response<Orcamento?>(orcamento, message: "Orçamento removido com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Orcamento?>(null, 500, message: "Não foi possível exlcuir o orçamento");
        }
        catch (Exception )
        {
            return new Response<Orcamento?>(null, 500, message: "Falha interna no servidor");
        }
    }

    public async Task<Response<OrcamentoViewModel?>> GetByIdAsync(GetOrcamentoByIdRequest request)
    {
        try
        {
            var orcamento = await context.Orcamentos
                .Include(x => x.Cliente)
                .Include(x => x.Veiculo)
                .Include(x => x.OrcamentoProdutos)
                .ThenInclude(x => x.Produto)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            var orcamentoViewModel = orcamento is null
                ? null
                : new OrcamentoViewModel()
                {
                    Id = orcamento.Id,
                    Cliente = new ClienteViewModel()
                    {
                        Id = orcamento.Cliente.Id,
                        Nome = orcamento.Cliente.Nome,
                        Telefone = orcamento.Cliente.Telefone,
                        CreatedAt = orcamento.Cliente.CreatedAt,
                        UpdatedAt = orcamento.Cliente.UpdatedAt
                    },
                    Veiculo = new VeiculoViewModel()
                    {
                        Id = orcamento.Veiculo.Id,
                        Placa = orcamento.Veiculo.Placa,
                        Nome = orcamento.Veiculo.Nome,
                        Ano = orcamento.Veiculo.Ano,
                        CreatedAt = orcamento.Veiculo.CreatedAt,
                        UpdatedAt = orcamento.Veiculo.UpdatedAt
                    },
                    ItensOrcamento = orcamento.OrcamentoProdutos.Select(x => new ProdutoOrcamentoViewModel()
                    {
                        Id = x.Id,
                        ProdutoId = x.ProdutoId,
                        Sku = x.Produto.Sku,
                        Nome = x.Produto.Nome,
                        Fabricante = x.Produto.Fabricante,
                        Quantidade = x.Quantidade,
                        ValorVenda = x.ValorVenda
                    }).ToList(),
                    Status = orcamento.Status,
                    CreatedAt = orcamento.CreatedAt,
                    UpdatedAt = orcamento.UpdatedAt
                };
            
            return orcamento is null
                ? new Response<OrcamentoViewModel?>(null, 404, message: "Orçamento não encontrado")
                : new Response<OrcamentoViewModel?>(orcamentoViewModel);
        }
        catch (DbUpdateException)
        {
            return new Response<OrcamentoViewModel?>(null, 500, message: "Não foi possível buscar o orçamento");
        }
        catch (Exception )
        {
            return new Response<OrcamentoViewModel?>(null, 500, message: "Falha interna no servidor");
        }
    }

    public async Task<PagedResponse<IEnumerable<OrcamentoViewModel>?>> GetAllAsync(GetAllOrcamentoRequest request)
    {
        try
        {
            var query = context.Orcamentos.AsNoTracking();
            
            var orcamentos = await query
                .Include(x => x.Cliente)
                .Include(x => x.Veiculo)
                .Skip((request.PageNumber -1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var orcamentoViewModel = orcamentos.Select(x => new OrcamentoViewModel()
            {
                Id = x.Id,
                Cliente = new ClienteViewModel()
                {
                    Id = x.Cliente.Id,
                    Nome = x.Cliente.Nome,
                    Telefone = x.Cliente.Telefone,
                    CreatedAt = x.Cliente.CreatedAt,
                    UpdatedAt = x.Cliente.UpdatedAt
                },
                Veiculo = new VeiculoViewModel()
                {
                    Id = x.Veiculo.Id,
                    Placa = x.Veiculo.Placa,
                    Nome = x.Veiculo.Nome,
                    Ano = x.Veiculo.Ano,
                    CreatedAt = x.Veiculo.CreatedAt,
                    UpdatedAt = x.Veiculo.UpdatedAt
                },
                Status = x.Status,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            });
            
            
            
            var count = await query.CountAsync();

            return new PagedResponse<IEnumerable<OrcamentoViewModel>?>(
                orcamentoViewModel,
                count,
                request.PageNumber,
                request.PageSize
            );
        }
        catch (DbUpdateException)
        {
            return new PagedResponse<IEnumerable<OrcamentoViewModel>?>(null, 500, message: "Não foi possível buscar os orçamentos");
        }
        catch (Exception )
        {
            return new PagedResponse<IEnumerable<OrcamentoViewModel>?>(null, 500, message: "Falha interna no servidor");
        }
    }

    public async Task<Response<Orcamento?>> AdicionarProdutoOrcamentoAsync(AdicionarProdutoOrcamentoRequest request)
    {
        try
        {
            var orcamento = await context.Orcamentos
                .Include(x => x.OrcamentoProdutos)
                .FirstOrDefaultAsync(x => x.Id == request.OrcamentoId);
            if (orcamento is null)
                return new Response<Orcamento?>(null, 404, message: "Orçamento não encontrado");

            var produto = context.Produtos.FirstOrDefault(x => x.Id == request.ProdutoId);
            if (produto is null)
                return new Response<Orcamento?>(null, 404, message: "Produto não encontrado");

            var item = new OrcamentoProduto()
            {
                Orcamento = orcamento,
                Produto = produto,
                Quantidade = request.Quantidade,
                ValorVenda = request.ValorVenda
            };
            
            
            orcamento.OrcamentoProdutos.Add(item);
            context.Orcamentos.Update(orcamento);
            await context.SaveChangesAsync();

            return new Response<Orcamento?>(orcamento, message: "Produto adicionado ao orçamento com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Orcamento?>(null, 500, message: "Não foi possível incluir o produto no orçamento");
        }
        catch (Exception )
        {
            return new Response<Orcamento?>(null, 500, message: "Falha interna no servidor");
        }
    }

    public async Task<Response<Orcamento?>> UpdateProdutoOrcamentoAsync(UpdateProdutoOrcamentoRequest request)
    {
        try
        {
            var orcamento = await context.Orcamentos
                .Include(x => x.OrcamentoProdutos)
                .FirstOrDefaultAsync(x => x.Id == request.OrcamentoId);
            
            if (orcamento is null)
                return new Response<Orcamento?>(null, 404, message: "Orçamento não encontrado");
        
            var item = orcamento.OrcamentoProdutos.FirstOrDefault(x => x.Id == request.Id);
            if (item is null)
                return new Response<Orcamento?>(null, 404, message: "Item não encontrado");
            
            item.Quantidade = request.Quantidade;
            item.ValorVenda = request.ValorVenda;
            
            
            context.Orcamentos.Update(orcamento);
            await context.SaveChangesAsync();
            
            return new Response<Orcamento?>(orcamento, message: "Item atualizado com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Orcamento?>(null, 500, message: "Não foi possível atualizar o produto no orçamento");
        }
        catch (Exception )
        {
            return new Response<Orcamento?>(null, 500, message: "Falha interna no servidor");
        }
    }

    public async Task<Response<Orcamento?>> RemoverProdutoOrcamentoAsync(RemoverProdutoOrcamentoRequest request)
    {
        try
        {
            var orcamento = await context.Orcamentos
                .Include(x => x.OrcamentoProdutos)
                .FirstOrDefaultAsync(x => x.Id == request.OrcamentoId);
            
            if (orcamento is null)
                return new Response<Orcamento?>(null, 404, message: "Orçamento não encontrado");
        
            var item = orcamento.OrcamentoProdutos.FirstOrDefault(x => x.Id == request.Id);
            if (item is null)
                return new Response<Orcamento?>(null, 404, message: "Item não encontrado");
            
            orcamento.OrcamentoProdutos.Remove(item);
            
            context.Orcamentos.Update(orcamento);
            context.OrcamentoProdutos.Remove(item);
            await context.SaveChangesAsync();
            
            return new Response<Orcamento?>(orcamento, message: "Item removido com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Orcamento?>(null, 500, message: "Não foi possível remover o produto do orçamento");
        }
        catch (Exception )
        {
            return new Response<Orcamento?>(null, 500, message: "Falha interna no servidor");
            
        }
        
    }
}