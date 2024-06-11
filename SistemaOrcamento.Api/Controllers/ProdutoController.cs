using Microsoft.AspNetCore.Mvc;
using SistemaOrcamento.Core;
using SistemaOrcamento.Core.Handlers;
using SistemaOrcamento.Core.Request.Produtos;

namespace SistemaOrcamento.Api.Controllers;

[ApiController]
[Route("v1/produtos")]
public class ProdutoController(IProdutoHandler handler) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProdutoRequest request)
    {
        
        var response = await handler.CreateAsync(request);

        return response.IsSuccess
            ? Created($"v1/produtos/{response.Data?.Id}", response)
            : BadRequest(response);
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
    )
    {
        var request = new GetAllProdutoRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var response = await handler.GetAllAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var request = new GetProdutoByIdRequest { Id = id };

        var response = await handler.GetByIdAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> SearchBySkuNome([FromQuery] string query)
    {
        var request = new SearchProdutoSkuNomeRequest { Query = query };

        var response = await handler.SearchBySkuNomeAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateProdutoRequest request)
    {
        request.Id = id;

        var response = await handler.UpdateAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var request = new DeleteProdutoRequest { Id = id };

        var response = await handler.DeleteAsync(request);

        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }
    
}