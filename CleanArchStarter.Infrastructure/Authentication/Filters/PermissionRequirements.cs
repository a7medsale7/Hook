using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Infrastructure.Authentication.Filters;
public class PermissionRequirements(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;






    public static IList<string?> GetAllPermissions() =>
        typeof(Permissions).GetFields().Select(f => f.GetValue(f) as string).ToList();
}
