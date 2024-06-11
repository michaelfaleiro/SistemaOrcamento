using Microsoft.AspNetCore.Mvc;
using SistemaOrcamento.Api.Handlers;
using SistemaOrcamento.Core;
using SistemaOrcamento.Core.Request.Clientes;

namespace SistemaOrcamento.Api.Controllers;

[ApiController]
[Route("v1/clientes")]
public class ClienteController(ClienteHandler handler) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateClienteRequest request)
    {
        var response = await handler.CreateAsync(request);
        
        return response.IsSuccess
            ? Created($"v1/clientes/{response.Data?.Id}", response) 
            : BadRequest(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
    )
    {
        var request = new GetAllClienteRequest
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
        var request = new GetClienteByIdRequest { Id = id };
        
        var response = await handler.GetByIdComVeiculoAsync(request);
        
        return response.IsSuccess
            ? Ok(response) 
            : BadRequest(response);
    }
    
    [HttpGet("search/")]
    public async Task<IActionResult> GetByNomeTelefonePlaca(
        [FromQuery] string query
    )
    {
        var request = new GetClienteByNomeTelefonePlacaRequest
        {
            Query = query
        };
        
        var response = await handler.GetByNomeTelefonePlacaAsync(request);
        
        return response.IsSuccess
            ? Ok(response) 
            : BadRequest(response);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put([FromRoute] int id, UpdateClienteRequest request)
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
        var request = new DeleteClienteRequest
        {
            Id = id
        };
        
        var response = await handler.DeleteAsync(request);
        
        return response.IsSuccess
            ? Ok(response) 
            : BadRequest(response);
    }
    
    [HttpPost("adicionar-veiculo")]
    public async Task<IActionResult> AddVeiculo([FromBody] AdicionarVeiculoClienteRequest request)
    {
        var response = await handler.AdicionarVeiculoAsync(request);
        
        return response.IsSuccess
            ? Ok(response) 
            : BadRequest(response);
    }
}