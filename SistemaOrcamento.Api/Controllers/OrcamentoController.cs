using Microsoft.AspNetCore.Mvc;
using SistemaOrcamento.Core;
using SistemaOrcamento.Core.Handlers;
using SistemaOrcamento.Core.Request.Orcamentos;

namespace SistemaOrcamento.Api.Controllers;

[ApiController]
[Route("v1/orcamentos")]
public class OrcamentoController(IOrcamentoHandler orcamentoHandler) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateOrcamentoRequest request)
    {
        var response = await orcamentoHandler.CreateAsync(request);

        return response.IsSuccess
            ? Created($"v1/orcamentos/{response.Data?.Id}", response)
            : BadRequest(response);
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
    )
    {
        var request = new GetAllOrcamentoRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var response = await orcamentoHandler.GetAllAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var request = new GetOrcamentoByIdRequest { Id = id };

        var response = await orcamentoHandler.GetByIdAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateOrcamentoRequest request)
    {
        request.Id = id;

        var response = await orcamentoHandler.UpdateAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var request = new DeleteOrcamentoRequest { Id = id };

        var response = await orcamentoHandler.DeleteAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpPost("adicionar-produto")]
    public async Task<IActionResult> AdicionarProduto([FromBody] AdicionarProdutoOrcamentoRequest request)
    {
        var response = await orcamentoHandler.AdicionarProdutoOrcamentoAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }
    
    [HttpPut("atualizar-produto")]
    public async Task<IActionResult> AtualizarProduto([FromBody] UpdateProdutoOrcamentoRequest request)
    {
        var response = await orcamentoHandler.UpdateProdutoOrcamentoAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }
    
    [HttpDelete("remover-produto")]
    public async Task<IActionResult> RemoverProduto([FromBody] RemoverProdutoOrcamentoRequest request)
    {
        var response = await orcamentoHandler.RemoverProdutoOrcamentoAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }
    
    

}