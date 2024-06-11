using Microsoft.AspNetCore.Mvc;
using SistemaOrcamento.Core;
using SistemaOrcamento.Core.Handlers;
using SistemaOrcamento.Core.Request.Veiculos;

namespace SistemaOrcamento.Api.Controllers
{
    [ApiController]
    [Route("v1/veiculos")]
    public class VeiculoController(IVeiculoHandler veiculoHandler) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateVeiculoRequest request)
        {
            var response = await veiculoHandler.CreateAsync(request);
            return response.IsSuccess
                ? Created($"v1/veiculos/{response.Data?.Id}", response)
                : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllVeiculoRequest()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            
            var response = await veiculoHandler.GetAllAsync(request);
            
            return response.IsSuccess
                ? Ok(response)
                : BadRequest(response);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var request = new GetVeiculoByIdRequest { Id = id };
            
            var response = await veiculoHandler.GetByIdAsync(request);
            
            return response.IsSuccess
                ? Ok(response)
                : BadRequest(response);
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, UpdateVeiculoRequest request)
        {
            request.Id = id;
            
            var response = await veiculoHandler.UpdateAsync(request);
            
            return response.IsSuccess
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var request = new DeleteVeiculoRequest { Id = id };
            
            var response = await veiculoHandler.DeleteAsync(request);
            
            return response.IsSuccess
                ? Ok(response)
                : BadRequest(response);
        }

        
    }
}