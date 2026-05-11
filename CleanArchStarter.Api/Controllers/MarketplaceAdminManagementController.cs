using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hook.Api.Controllers
{
    [ApiController]
    [Route("api/marketplace/admin")]
    public class MarketplaceAdminManagementController(IMarketplaceAdminManagementService adminManagementService) : ControllerBase
    {
        [HttpGet("sellers/GetAll")]
        [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.MarketplaceAdmin_ViewSellers)]
        public async Task<IActionResult> GetAllSellers(CancellationToken cancellationToken)
        {
            var result = await adminManagementService.GetAllSellersAsync(cancellationToken);
            return Ok(result.Value);
        }

        [HttpDelete("sellers/delete/{sellerProfileId:guid}")]
        [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.MarketplaceAdmin_DeleteSeller)]
        public async Task<IActionResult> DeleteSeller(Guid sellerProfileId, CancellationToken cancellationToken)
        {
            var result = await adminManagementService.DeleteSellerAsync(sellerProfileId, cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpGet("products/GetAll")]
        [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.MarketplaceAdmin_ViewProducts)]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
        {
            var result = await adminManagementService.GetAllProductsAsync(cancellationToken);
            return Ok(result.Value);
        }

        [HttpDelete("products/delete/{productId:guid}")]
        [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.MarketplaceAdmin_DeleteProduct)]
        public async Task<IActionResult> DeleteProduct(Guid productId, CancellationToken cancellationToken)
        {
            var result = await adminManagementService.DeleteProductAsync(productId, cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }
}
