using CryptLearn.Modules.AccessControl.Core.Entities;
using CryptLearn.Modules.AccessControl.Core.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace CryptLearn.Modules.AccessControl.Unit.Moqs
{
    internal class TestTokenHandler : IJwtTokenHandler
    {
        private Queue<TokenValidationResult> _pendingResults;

        public void SetPendingResults(params bool[] results)
        {
            var validationResults = results.Select(x => new TokenValidationResult() { IsValid = x });
            _pendingResults = new(validationResults);
        }

        public Task<TokenValidationResult> ValidateTokenAsync(string token, TokenValidationParameters validationParameters)
        {
            return Task.FromResult(_pendingResults.Dequeue());
        }

        public string WriteToken(JwtSecurityToken jwt)
        {
            throw new NotImplementedException();
        }
    }
}
