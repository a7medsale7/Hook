using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchStarter.Application.Contracts.Auth;
public class RefreshTokenRequest
{
    public string Token { get; set; }

    public string RefreshToken { get; set; }

}
