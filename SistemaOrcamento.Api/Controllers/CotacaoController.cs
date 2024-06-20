using Microsoft.AspNetCore.Mvc;
using SistemaOrcamento.Core;
using SistemaOrcamento.Core.Handlers;
using SistemaOrcamento.Core.Request.Cotacao;

namespace SistemaOrcamento.Api.Controllers;

[ApiController]
[Route("cotacoes")]
public class CotacaoController(ICotacaoHandler cotacaoHandler) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCotacaoRequest request)
    {
        var response = await cotacaoHandler.CreateAsync(request);

        return response.IsSuccess
            ? Created($"cotacoes/{response.Data?.Id}", response)
            : BadRequest(response);
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
    )
    {
        var request = new GetAllCotacaoRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var response = await cotacaoHandler.GetAllAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var request = new GetCotacaoByIdRequest { Id = id };

        var response = await cotacaoHandler.GetByIdAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }
    

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var request = new DeleteCotacaoRequest { Id = id };

        var response = await cotacaoHandler.DeleteAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpPost("{id:int}/produtos")]
    public async Task<IActionResult> AdicionarProdutoCotacao([FromRoute] int id, [FromBody] AdicionarProdutoCotacaoRequest request)
    {
        request.CotacaoId = id;

        var response = await cotacaoHandler.AdicionarProdutoCotacaoAsync(request);

        return response.IsSuccess
            ? Created($"cotacoes/{id}/produtos/{response.Data?.Id}", response)
            : BadRequest(response);
    }

    [HttpPut("{id:int}/produtos/{produtoId:int}")]
    public async Task<IActionResult> UpdateProdutoCotacao([FromRoute] int id, [FromRoute] int produtoId, [FromBody] UpdateProdutoCotacaoRequest request)
    {
        request.CotacaoId = id;
        request.ProdutoId = produtoId;

        var response = await cotacaoHandler.UpdateProdutoCotacoAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }
    
    [HttpDelete("{id:int}/produtos/{produtoId:int}")]
    public async Task<IActionResult> RemoverProdutoCotacao([FromRoute] int id, [FromRoute] int produtoId)
    {
        var request = new RemoverProdutoCotacaoRequest
        {
            CotacaoId = id,
            ProdutoId = produtoId
        };

        var response = await cotacaoHandler.RemoverProdutoCotacaoAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }
    
}