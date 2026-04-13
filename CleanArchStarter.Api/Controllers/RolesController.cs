using Hook.Application.Contracts.Roles;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Hook.Infrastructure.Authentication.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hook.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = DefaultRoles.Admin)] // ????? ?????? ??? Admin ??? ?????? ???????
public class RolesController(IRoleService _roleService) : ControllerBase
{
    private readonly IRoleService roleService = _roleService;

    // ??? ?? ???????
    [HttpGet("admin/allroles/GetAll")]
    [HasPermission(Permissions.Roles_View)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDisabled = false)
    {
        var result = await roleService.GetAllAsync(includeDisabled);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    // ??? ??? ???? ????????? ?????????? ???????? ??
    [HttpGet("admin/{id}")]
    [HasPermission(Permissions.Roles_View)]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        var result = await roleService.GetByIdAsync(id);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    // ??? ????? ??? ????????? ??????? ?? ??????
    // ????? ???? ??? Frontend ??? ??? ??? Checkboxes ?????? ?? ????? ???
    [HttpGet("admin/permissions")]
    [HasPermission(Permissions.Roles_View)]
    public IActionResult GetPermissions()
    {
        var permissions = Permissions.GetAllPermissions();
        return Ok(permissions);
    }

    // ????? ??? ???? ?? ????? ????? ????????? ?????? ??
    [HttpPost("admin/add")]
    [HasPermission(Permissions.Roles_Create)]
    public async Task<IActionResult> Add([FromBody] RoleReqest request)
    {
        var result = await roleService.AddAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    // ????? ??? ????? ?? ????? ????? ????????? (Sync Permissions)
    [HttpPut("admin/update/{id}")]
    [HasPermission(Permissions.Roles_Update)]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] RoleReqest request)
    {
        var result = await roleService.UpdateAsync(id, request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    // ????? ?? ????? (Soft Delete) ?????
    [HttpPatch("admin/toggle-status/{id}")]
    [HasPermission(Permissions.Roles_ToggleActive)]
    public async Task<IActionResult> ToggleStatus([FromRoute] string id)
    {
        var result = await roleService.ToggleStatusAsync(id);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}
