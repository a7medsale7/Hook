using CleanArchStarter.Domain.Consts;
using CleanArchStarter.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchStarter.Infrastructure.Authentication;
public interface IJwtProvider
{
    (string Token, DateTime Expiration) GenerateToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions);

    string? ValidateToken(string token);


}