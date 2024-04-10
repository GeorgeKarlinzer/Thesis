using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace CryptLearn.Modules.AccessControl.Core.Services
{
    internal class JwtTokenHandlerWrapper : IJwtTokenHandler
    {
        private readonly JwtSecurityTokenHandler _handler;

        public JwtTokenHandlerWrapper()
        {
            _handler = new JwtSecurityTokenHandler();
        }

        public Task<TokenValidationResult> ValidateTokenAsync(string token, TokenValidationParameters validationParameters)
        {
            return _handler.ValidateTokenAsync(token, validationParameters);
        }

        public string WriteToken(JwtSecurityToken jwt)
        {
            return _handler.WriteToken(jwt);
        }
    }
}
