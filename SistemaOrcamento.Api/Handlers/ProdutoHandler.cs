using Microsoft.EntityFrameworkCore;
using SistemaOrcamento.Api.Data;
using SistemaOrcamento.Core.Handlers;
using SistemaOrcamento.Core.Models;
using SistemaOrcamento.Core.Request.Produtos;
using SistemaOrcamento.Core.Response;

namespace SistemaOrcamento.Api.Handlers;

public class ProdutoHandler(AppDbContext context) : IProdutoHandler
{
    public async Task<Response<Produto?>> CreateAsync(CreateProdutoRequest request)
    {
        var produto = new Produto()
        {
            Sku = request.Sku,
            Nome = request.Nome,
            ValorVenda = request.ValorVenda
        };

        try
        {
            await context.Produtos.AddAsync(produto);
            await context.SaveChangesAsync();
            
            return new Response<Produto?>(produto, 201);
        }
        catch (DbUpdateException)
        {
            return new Response<Produto?>(null, message: "Erro ao salvar o produto.");
        }
        catch (Exception)
        {
            return new Response<Produto?>(null, message: "Erro inesperado ao salvar o produto.");
        }
    }

    public async Task<Response<Produto?>> UpdateAsync(UpdateProdutoRequest request)
    {
        try
        {

            var produto = await context.Produtos.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (produto == null)
            {
                return new Response<Produto?>(null, message: "Produto não encontrado.");
            }

            produto.Sku = request.Sku;
            produto.Nome = request.Nome;
            produto.ValorVenda = request.ValorVenda;
            produto.UpdatedAt = DateTime.UtcNow;

            context.Produtos.Update(produto);
            await context.SaveChangesAsync();

            return new Response<Produto?>(produto, message: "Produto atualizado com sucesso.");
        }
        catch (DbUpdateException)
        {
            return new Response<Produto?>(null, message: "Erro ao atualizar o produto.");
        }
        catch (Exception)
        {
            return new Response<Produto?>(null, message: "Falha interna no servidor.");
        }
        
    }

    public async Task<Response<Produto?>> DeleteAsync(DeleteProdutoRequest request)
    {
        try
        {
            var produto = await context.Produtos.FirstOrDefaultAsync(x => x.Id == request.Id);

            if(produto is null)
                return new Response<Produto?>(null, 404, message: "Produto não encontrado.");
            

            context.Produtos.Remove(produto);
            await context.SaveChangesAsync();

            return new Response<Produto?>(null, message: "Produto removido com sucesso.");
        }
        catch (DbUpdateException)
        {
            return new Response<Produto?>(null, 500, message: "Erro ao remover o produto.");
        }
        catch (Exception)
        {
            return new Response<Produto?>(null,500, message: "Falha interna no servidor.");
        }
    }

    public async Task<Response<Produto?>> GetByIdAsync(GetProdutoByIdRequest request)
    {
        try
        {
            var produto = await context.Produtos.FirstOrDefaultAsync(x => x.Id == request.Id);
            
            return produto is null
                ? new Response<Produto?>(null, 404, message: "Produto não encontrado.")
                : new Response<Produto?>(produto);
        }
        catch (DbUpdateException)
        {
            return new Response<Produto?>(null, 500, message: "Erro ao buscar o produto.");
        }
        catch (Exception)
        {
            return new Response<Produto?>(null, 500, message: "Falha interna no servidor.");
        }
    }

    public async Task<PagedResponse<IEnumerable<Produto>?>> GetAllAsync(GetAllProdutoRequest request)
    {
        try
        {
            var query = context.Produtos.AsNoTracking();
            
            var produtos = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResponse<IEnumerable<Produto>?>(produtos);
        }
        catch (DbUpdateException)
        {
            return new PagedResponse<IEnumerable<Produto>?>(null, 500, message: "Erro ao buscar os produtos.");
        }
        catch (Exception)
        {
            return new PagedResponse<IEnumerable<Produto>?>(null, 500, message: "Falha interna no servidor.");
        }
    }

    public async Task<PagedResponse<IEnumerable<Produto>?>> SearchBySkuNomeAsync(SearchProdutoSkuNomeRequest request)
    {
        try
        {
            var query = context.Produtos
                .AsNoTracking()
                .Where(p => p.Sku.Contains(request.Query) || p.Nome.Contains(request.Query));

            var produtos = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<IEnumerable<Produto>?>(produtos, count, request.PageNumber, request.PageSize);
        }
        catch (DbUpdateException)
        {
            return new PagedResponse<IEnumerable<Produto>?>(null, 500, message: "Erro ao buscar os produtos.");
        }
        catch (Exception)
        {
            return new PagedResponse<IEnumerable<Produto>?>(null, 500, message: "Falha interna no servidor.");
        }
    }
}
