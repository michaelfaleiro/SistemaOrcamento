using Microsoft.EntityFrameworkCore;
using SistemaOrcamento.Api.Data;
using SistemaOrcamento.Core.Handlers;
using SistemaOrcamento.Core.Models;
using SistemaOrcamento.Core.Request.Cotacao;
using SistemaOrcamento.Core.Response;
using SistemaOrcamento.Core.ViewsModels.Cotacao;

namespace SistemaOrcamento.Api.Handlers;

public class CotacaoHandler(AppDbContext context) : ICotacaoHandler
{
    public async Task<Response<Cotacao?>> CreateAsync(CreateCotacaoRequest request)
    {
        var orcamento = await context.Orcamentos.FirstOrDefaultAsync(x=> x.Id == request.OrcamentoId);
        if (orcamento is null)
            return new Response<Cotacao?>(null, 500, "Orçamento não encontrado");

        var cotacao = new Cotacao()
        {
            Orcamento = orcamento,
        };

        try
        {
            await context.Cotacoes.AddAsync(cotacao);
            await context.SaveChangesAsync();
            
            return new Response<Cotacao?>(cotacao, 201, "Cotação criada com sucesso");

        }
        catch (DbUpdateException)
        {
            return new Response<Cotacao?>(null, 500, "Erro ao salvar no banco de dados");
        }
        catch (Exception)
        {
            return new Response<Cotacao?>(null, 500, "Falha interna no servidor");
        }
        
    }
    
    public async Task<Response<Cotacao?>> DeleteAsync(DeleteCotacaoRequest request)
    {
        var cotacao = await context.Cotacoes.FirstOrDefaultAsync(x=> x.Id == request.Id);
        if (cotacao is null)
            return new Response<Cotacao?>(null, 400, "Cotação não encontrada");
        
        try
        {
            context.Cotacoes.Remove(cotacao);
            await context.SaveChangesAsync();
            return new Response<Cotacao?>(cotacao, 200, "Cotação removida com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Cotacao?>(null, 500, "Erro ao salvar no banco de dados");
        }
        catch (Exception)
        {
            return new Response<Cotacao?>(null, 500, "Falha interna no servidor");
        }
    }

    public async Task<Response<CotacaoViewModel?>> GetByIdAsync(GetCotacaoByIdRequest request)
{
    try
    {
        var cotacao = await context.Cotacoes
            .Include(x => x.CotacaoProdutos)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (cotacao is null)
        {
            return new Response<CotacaoViewModel?>(null, 400, "Cotação não encontrada");
        }

        var cotacaoViewModel = new CotacaoViewModel
        {
            Id = cotacao.Id,
            CreatedAt = cotacao.CreatedAt,
            UpdatedAt = cotacao.UpdatedAt,
            CotacaoProdutos = cotacao.CotacaoProdutos.Select(cp => new CotacaoProdutoViewModel
            {
                Id = cp.Id,
                Nome = cp.Nome,
                Sku = cp.Sku,
                CreatedAt = cp.CreatedAt,
                UpdatedAt = cp.UpdatedAt
            })
        };

        return new Response<CotacaoViewModel?>(cotacaoViewModel);
    }
    catch (DbUpdateException)
    {
        return new Response<CotacaoViewModel?>(null, 500, "Não foi possível buscar a cotação");
    }
    catch (Exception)
    {
        return new Response<CotacaoViewModel?>(null, 500, "Falha interna no servidor");
    }
   
}

    public async Task<PagedResponse<IEnumerable<CotacaoViewModel>?>> GetAllAsync(GetAllCotacaoRequest request)
    {

        try
        {
            var query = context.Cotacoes
                .AsNoTracking()
                .Include(x => x.CotacaoProdutos);
        
            var cotacoes = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(c => new CotacaoViewModel
                {
                    Id = c.Id,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    CotacaoProdutos = c.CotacaoProdutos.Select(cp => new CotacaoProdutoViewModel
                    {
                        Id = cp.Id,
                        Nome = cp.Nome,
                        Sku = cp.Sku,
                        CreatedAt = cp.CreatedAt,
                        UpdatedAt = cp.UpdatedAt
                    })
                })
                .ToListAsync();
        
            var count = await query.CountAsync();
            
            return new PagedResponse<IEnumerable<CotacaoViewModel>?>(cotacoes, count, request.PageNumber, request.PageSize);
        }
        catch (DbUpdateException)
        {
            return new PagedResponse<IEnumerable<CotacaoViewModel>?>(null, 500, message: "Não foi possível buscar as cotações");
        }
        catch (Exception)
        {
            return new PagedResponse<IEnumerable<CotacaoViewModel>?>(null, 500, message: "Falha interna no servidor");
        }
        
    }

    public async Task<Response<Cotacao?>> AdicionarProdutoCotacaoAsync(AdicionarProdutoCotacaoRequest request)
    {
        var cotacao = await context.Cotacoes.FirstOrDefaultAsync(x=> x.Id == request.CotacaoId);
        if (cotacao is null)
            return new Response<Cotacao?>(null, 400, "Cotação não encontrada");

        var produto = new CotacaoProduto()
        {
            Cotacao = cotacao,
            Nome = request.Nome,
            Sku = request.Sku,
        };
        
        cotacao.CotacaoProdutos.Add(produto);
        
        try
        {
            await context.SaveChangesAsync();
            return new Response<Cotacao?>(cotacao, 200, "Produto adicionado com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Cotacao?>(null, 500, "Erro ao salvar no banco de dados");
        }
        catch (Exception)
        {
            return new Response<Cotacao?>(null, 500, "Falha interna no servidor");
        }
    }
    
    public async Task<Response<Cotacao?>> UpdateProdutoCotacoAsync(UpdateProdutoCotacaoRequest request)
    {
        var cotacao = await context.Cotacoes
            .Include(x=> x.CotacaoProdutos)
            .FirstOrDefaultAsync(x=> x.Id == request.CotacaoId);
        
        if (cotacao is null)
            return new Response<Cotacao?>(null, 400, "Cotação não encontrada");

        var produto = cotacao.CotacaoProdutos.FirstOrDefault(x=> x.Id == request.ProdutoId);
        if (produto is null)
            return new Response<Cotacao?>(null, 400, "Produto não encontrado");

        produto.Nome = request.Nome;
        produto.Sku = request.Sku;
        produto.UpdatedAt = DateTime.UtcNow;
        
        try
        {
            await context.SaveChangesAsync();
            return new Response<Cotacao?>(cotacao, 200, "Produto atualizado com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Cotacao?>(null, 500, "Erro ao salvar no banco de dados");
        }
        catch (Exception)
        {
            return new Response<Cotacao?>(null, 500, "Falha interna no servidor");
        }
    }

    public async Task<Response<Cotacao?>> RemoverProdutoCotacaoAsync(RemoverProdutoCotacaoRequest request)
    {
        var cotacao = await context.Cotacoes
            .Include(x=> x.CotacaoProdutos)
            .FirstOrDefaultAsync(x=> x.Id == request.CotacaoId);
        
        if (cotacao is null)
            return new Response<Cotacao?>(null, 400, "Cotação não encontrada");

        var produto = cotacao.CotacaoProdutos.FirstOrDefault(x=> x.Id == request.ProdutoId);
        if (produto is null)
            return new Response<Cotacao?>(null, 400, "Produto não encontrado");

        cotacao.CotacaoProdutos.Remove(produto);
        
        try
        {
            await context.SaveChangesAsync();
            return new Response<Cotacao?>(cotacao, 200, "Produto removido com sucesso");
        }
        catch (DbUpdateException)
        {
            return new Response<Cotacao?>(null, 500, "Erro ao salvar no banco de dados");
        }
        catch (Exception)
        {
            return new Response<Cotacao?>(null, 500, "Falha interna no servidor");
        }
    }
}