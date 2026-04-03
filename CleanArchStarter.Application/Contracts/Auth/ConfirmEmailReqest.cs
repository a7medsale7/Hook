using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchStarter.Application.Contracts.Auth;
public class ConfirmEmailReqest
{
    public string UserId { get; set; } = null!;
    public string Code { get; set; } = null!;
}