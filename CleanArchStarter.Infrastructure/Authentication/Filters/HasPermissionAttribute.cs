using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Infrastructure.Authentication.Filters;
public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
}
