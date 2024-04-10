using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace CryptLearn.Modules.AccessControl.Core.Services
{
    public interface IJwtTokenHandler
    {
        Task<TokenValidationResult> ValidateTokenAsync(string token, TokenValidationParameters validationParameters);
        string WriteToken(JwtSecurityToken jwt);
    }
}
