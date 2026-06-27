using Hook.Application.Contracts.Marketplace.Products;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hook.Api.Controllers
{
    [ApiController]
    [Route("api/marketplace/seller/products")]
    public class MarketplaceSellerProductsController(IMarketplaceSellerProductService sellerProductService) : ControllerBase
    {
        [HttpPost("seller/create")]
        [Authorize(Policy = Permissions.MarketplaceProducts_Create)]
        public async Task<IActionResult> Create([FromForm] CreateMarketplaceProductRequest request, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var result = await sellerProductService.CreateAsync(userId, request, cancellationToken);
            return result.IsSuccess ? Ok(new { productId = result.Value }) : BadRequest(result.Error);
        }

        [HttpPut("seller/update")]
        [Authorize(Policy = Permissions.MarketplaceProducts_Update)]
        public async Task<IActionResult> Update([FromForm] UpdateMarketplaceProductRequest request, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var result = await sellerProductService.UpdateAsync(userId, request, cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpDelete("seller/delete/{productId:guid}")]
        [Authorize(Policy = Permissions.MarketplaceProducts_Delete)]
        public async Task<IActionResult> Delete(Guid productId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var result = await sellerProductService.DeleteAsync(userId, productId, cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpGet("seller/my-products")]
        [Authorize(Policy = Permissions.MarketplaceProducts_View)]
        public async Task<IActionResult> MyProducts([FromQuery] bool? isActive, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var result = await sellerProductService.GetMyProductsAsync(userId, isActive, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }

}
