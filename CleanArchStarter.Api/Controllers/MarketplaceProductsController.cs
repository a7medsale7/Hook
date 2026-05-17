using Hook.Application.Contracts.Marketplace.Products;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hook.Api.Controllers
{
    [ApiController]
    [Route("api/marketplace/products")]
    public class MarketplaceProductsController(IMarketplaceProductService productService) : ControllerBase
    {
        [HttpGet("allroles/search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] MarketplaceProductFilterRequest filter, CancellationToken cancellationToken)
        {
            var result = await productService.SearchAsync(filter, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("allroles/{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await productService.GetByIdAsync(id, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }
    }

}
