using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Abstractions.Result;
public record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error Forbidden = new("Error.Forbidden", "You do not have permission to perform this action.");
    public static readonly Error Unauthorized = new("Error.Unauthorized", "You are not authorized.");
}